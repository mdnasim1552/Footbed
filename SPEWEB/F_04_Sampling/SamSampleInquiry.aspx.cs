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
using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using System.Drawing.Drawing2D;

namespace SPEWEB.F_04_Sampling
{
    public partial class SamSampleInquiry : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();

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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sample  Development Inquiry Entry";
                CommonButton();
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string qgenno = this.Request.QueryString["genno"] ?? "";
                this.GetGenCode();
                this.Get_BuyerName();
                this.GetMaterial();
                this.DropdownSelectedMat();
                this.GetBatchList();
                if (qgenno.Length > 0)
                {
                    this.ibtnPreList_Click(null, null);
                    //this.ddlPreList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    this.lbtnOk_Click(null, null);
                }
             ;

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

        private void Get_BuyerName()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            DdlCustomer.DataTextField = "sirdesc";
            DdlCustomer.DataValueField = "sircode";
            DdlCustomer.DataSource = ds2.Tables[0];
            DdlCustomer.DataBind();
        }
        private void GetLastInqNumber()
        {

            string comcod = this.GetComCode();
            string date = this.txtDatefrom.Text;
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETLASTINQNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6);

            this.ddlPreList.DataTextField = "maxperno1";
            this.ddlPreList.DataValueField = "maxperno";
            this.ddlPreList.DataSource = ds3.Tables[0];
            this.ddlPreList.DataBind();

        }

        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetComCode();
            var lst = objUserMan.GetGenCode(comcod);          
            Session["lstgencode"] = lst;

            var lstcat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "11");
            lstcat.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstseason = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "33");
            lstseason.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstlformat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "41");
            lstlformat.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));
            var lststype = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "43");
            lststype.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));
            var lstBrand =lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "37");
             lstBrand.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));
            var lstsamptype = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "34");
            lstsamptype.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000","None"));

            var lstconstuction = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "31");
            lstconstuction.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstunit = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "21");
            lstunit.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstcolor = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "51");
            lstcolor.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            var lstversion = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "61");
           

            ddlForma.DataTextField = "gdesc";
            ddlForma.DataValueField = "gcod";
            ddlForma.DataSource = lstlformat;
            ddlForma.DataBind();

            DdlCategory.DataTextField = "gdesc";
            DdlCategory.DataValueField = "gcod";
            DdlCategory.DataSource = lstcat;
            DdlCategory.DataBind();
            
            
            DdlShoType.DataTextField = "gdesc";
            DdlShoType.DataValueField = "gcod";
            DdlShoType.DataSource = lststype;
            DdlShoType.DataBind();           

           
            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = lstseason;
            DdlSeason.DataBind();
            

            DdlAgent.DataTextField = "gdesc";
            DdlAgent.DataValueField = "gcod";
            DdlAgent.DataSource = lstagent;
            DdlAgent.DataBind();

            DdlSampType.DataTextField = "gdesc";
            DdlSampType.DataValueField = "gcod";
            DdlSampType.DataSource = lstsamptype;
            DdlSampType.DataBind();

            DdlBrand.DataTextField = "gdesc";
            DdlBrand.DataValueField = "gcod";
            DdlBrand.DataSource = lstBrand;
            DdlBrand.DataBind();

            DdlContruction.DataTextField = "gdesc";
            DdlContruction.DataValueField = "gcod";
            DdlContruction.DataSource = lstconstuction;
            DdlContruction.DataBind();

            DdlUnit.DataTextField = "gdesc";
            DdlUnit.DataValueField = "gcod";
            DdlUnit.DataSource = lstunit;
            DdlUnit.DataBind();

            DDlColor.DataTextField = "gdesc";
            DDlColor.DataValueField = "gcod";
            DDlColor.DataSource = lstcolor;
            DDlColor.DataBind();

            DdlVersion.DataTextField = "gdesc";
            DdlVersion.DataValueField = "gcod";
            DdlVersion.DataSource = lstversion;
            DdlVersion.DataBind();
            

        }
        private void GetMaterial()
        {

            Session.Remove("lstgetmaterial");
            string comcod = this.GetComCode();
            var lst = objUserMan.GetMaterail(comcod);
            Session["lstgetmaterial"] = lst;

        }
        private void DropdownSelectedMat()
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lst =  (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];

            //Upper, Line
            // var lstupmat = lst.FindAll(l => l.sircode.Substring(0, 7) == "0401001");
            var lstupmat = lst.FindAll(l => l.sircode.Substring(0, 4) == "0401");
            this.ddlUpperMat.DataTextField = "sirdesc";
            this.ddlUpperMat.DataValueField = "sircode";
            this.ddlUpperMat.DataSource = lstupmat;
            this.ddlUpperMat.DataBind();

            // var lstlimat = lst.FindAll(l => l.sircode.Substring(0, 7) == "0401002");
            var lstlimat = lst.FindAll(l => l.sircode.Substring(0, 4) == "0401");
            this.ddllineMat.DataTextField = "sirdesc";
            this.ddllineMat.DataValueField = "sircode";
            this.ddllineMat.DataSource = lstlimat;
            this.ddllineMat.DataBind();

            var lstskmat = lst.FindAll(l => l.sircode.Substring(0, 4) == "0401");
            this.ddlsockMat.DataTextField = "sirdesc";
            this.ddlsockMat.DataValueField = "sircode";
            this.ddlsockMat.DataSource = lstskmat;
            this.ddlsockMat.DataBind();

             var lstosmat = lst.FindAll(l => l.sircode.Substring(0, 4) == "0401");
            // var lstosmat = lst.FindAll(l => l.sircode.Substring(0, 7) == "0401024");
            this.ddloutsoleMat.DataTextField = "sirdesc";
            this.ddloutsoleMat.DataValueField = "sircode";
            this.ddloutsoleMat.DataSource = lstosmat;
            this.ddloutsoleMat.DataBind();



            var lstaccmat = lst.FindAll(l => l.sircode.Substring(0, 4) == "0405");
            this.ddlaccMat.DataTextField = "sirdesc";
            this.ddlaccMat.DataValueField = "sircode";
            this.ddlaccMat.DataSource = lstaccmat;
            this.ddlaccMat.DataBind();

        }

        public void CommonButton()
        {
          //  ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

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
                this.Show_Inquiry_Info();
                this.DetailsPanel.Visible = true;
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
            this.DetailsPanel.Visible = false;

        }
        private void Show_Inquiry_Info()
        {

            string comcod = this.GetComCode();
            ViewState.Remove("tblinquery");
            string CurDate1 = this.txtDatefrom.Text.Trim();
            string mInqNo = "NEWINQ";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtDatefrom.Enabled = false;
                mInqNo = this.ddlPreList.SelectedValue.ToString();
            }

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GET_SMPLE_INQ", mInqNo, CurDate1,
                          "", "", "", "", "", "", "");
            ViewState["tblinquery"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();
            ViewState["tblproinquery"] = ds1.Tables[2].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>();
        

            if ( ds1 == null)
            {
               
                return;
            }

            if (mInqNo == "NEWINQ")
            {

                DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETLASTINQNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6);

                this.Data_Bind();
                this.Modal_Data_Bind();
                return;
            }


            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["inqno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["inqno1"].ToString().Substring(6, 5);
            this.txtDatefrom.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["inqdat"]).ToString("dd-MMM-yyyy");
            this.Data_Bind();
            this.Modal_Data_Bind();

        }

        private void GetBatchList()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%1661%";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", txtSProject, "", "", "", "", "", "", "", "");
            this.DdlBatch.DataTextField = "actdesc1";
            this.DdlBatch.DataValueField = "actcode";
            this.DdlBatch.DataSource = ds1.Tables[0];
            this.DdlBatch.DataBind();

        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string srchsam = (qgenno.Length > 0 ? qgenno : "") + "%";

            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETPRE_INQUIRY_NO", curdate, srchsam, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


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
            var inqtbl = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];


            this.gvSampleInq.DataSource = inqtbl;
            this.gvSampleInq.DataBind();
            if (inqtbl.Count == 0)
                return;
            // by safi
            this.ddlForma.SelectedValue = inqtbl[0].lformacod.Trim();
            this.DdlCategory.SelectedValue = inqtbl[0].catagory.Trim();
            this.DdlShoType.SelectedValue = inqtbl[0].shoetype.Trim();
            this.DdlSeason.SelectedValue = inqtbl[0].season.Trim();
            this.DdlBrand.SelectedValue = inqtbl[0].brand.Trim();
            this.DdlSampType.SelectedValue = inqtbl[0].samptype.Trim();
            this.DdlAgent.SelectedValue = inqtbl[0].agentcod.Trim();
            this.DdlCustomer.SelectedValue = inqtbl[0].buyer.Trim();
            this.DdlContruction.SelectedValue = inqtbl[0].conscode.Trim();
            this.DdlVersion.SelectedValue = inqtbl[0].sversion.Trim();

            this.TxtArticle.Text = inqtbl[0].article;
            this.TxtAcces.Text = inqtbl[0].accessories;
            this.TxtSock.Text = inqtbl[0].skmaterial;
            this.TxtLinnig.Text = inqtbl[0].limaterial;
            this.TxtUppMat.Text = inqtbl[0].upmaterial;
            this.TxtOutsole.Text = inqtbl[0].osmaterial;
            this.TxtSampleSize.Text = inqtbl[0].samsize.ToString();
            this.TxtConSize.Text = inqtbl[0].comsize.ToString();
            this.TxtSizeRange.Text = inqtbl[0].sizerange;
            this.TxtDelDate.Text = inqtbl[0].deldate.ToString("dd-MMM-yyyy");
            this.TxtRemarks.Text = inqtbl[0].remarks;
            this.DdlUnit.SelectedValue = inqtbl[0].unit;
            this.TxtQty.Text = inqtbl[0].samqty.ToString("#,##;(#,##0); ");
            this.DdlBatch.SelectedValue = inqtbl[0].batchcode;
            this.DDlColor.SelectedValue = inqtbl[0].color;
            this.txtConOrdNo.Text = inqtbl[0].conordno;
            this.ImgSample.ImageUrl = (inqtbl[0].images.ToString() == "") ? "~/images/no_img_preview.png" : inqtbl[0].images;
            this.HypLinkImage.NavigateUrl = (inqtbl[0].images.ToString() == "") ? "~/images/no_img_preview.png" : inqtbl[0].images;

         
        }
        private void Save_Value()
        {
            var lstinq = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];
            int i = 0;
            foreach (SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling lst  in lstinq)
            {
                lstinq[i].lformacod = ddlForma.SelectedValue.ToString();
                lstinq[i].article = TxtArticle.Text.Trim().ToString();
                lstinq[i].catagory = DdlCategory.SelectedValue.ToString();
                lstinq[i].shoetype = DdlShoType.SelectedValue.ToString();
                lstinq[i].season = DdlSeason.SelectedValue.ToString();
                lstinq[i].buyer = DdlCustomer.SelectedValue.ToString();
                lstinq[i].samptype = DdlSampType.SelectedValue.ToString();
                lstinq[i].conscode = DdlContruction.SelectedValue.ToString();
                lstinq[i].brand = DdlBrand.SelectedValue.ToString();
                lstinq[i].agentcod = DdlAgent.SelectedValue.ToString();
                lstinq[i].samsize = ((TextBox)this.TxtSampleSize).Text.Trim().ToString();
                lstinq[i].comsize = ((TextBox)this.TxtConSize).Text.Trim().ToString();
                lstinq[i].sizerange = ((TextBox)this.TxtSizeRange).Text.ToString();
                lstinq[i].samqty = ASTUtility.StrPosOrNagative(((TextBox)this.TxtQty).Text.Trim());
                lstinq[i].remarks = this.TxtRemarks.Text.ToString();
                lstinq[i].deldate = Convert.ToDateTime(this.TxtDelDate.Text);
                lstinq[i].unit = DdlUnit.SelectedValue.ToString();
                lstinq[i].batchcode = DdlBatch.SelectedValue.ToString();
                lstinq[i].color = DDlColor.SelectedValue.ToString();
                lstinq[i].conordno = this.txtConOrdNo.Text.ToString();
                lstinq[i].sversion = this.DdlVersion.SelectedValue.ToString();

                //lstinq[i].remarks = ((TextBox)gvSampleInq.Rows[i].FindControl("txtgvremarks")).Text.ToString();
                i++;
            }
           
            
        }
      
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                this.Save_Value();
                string comcod = this.GetComCode();
                if (this.ddlPreList.Items.Count == 0)
                    this.GetLastInqNumber();
                string buyer ="";
                string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
                string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

                var inqtbl = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];
                if (inqtbl[0].article.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Article Name is Mandatory Field');", true);
                    return;
                }
                if (inqtbl[0].color == "00000")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Color Field');", true);
                    return;
                }
                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "CHECK_ARTICLE_NAME", 
                    inqtbl[0].article, inqno, inqtbl[0].color, inqtbl[0].buyer, inqtbl[0].sversion, inqtbl[0].samptype, "", "");

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Article Name already exist!!');", true);
                    return;
                }

                //  var inqtbl = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
                DataTable dt = ASITUtility03.ListToDataTable(inqtbl);
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tbl1";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string PostedByid = hst["usrid"].ToString();
                string Posttrmid = hst["compname"].ToString();
                string PostSession = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                string xml = ds.GetXml();
                //return;
                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSORUPDATEPROTOSAMPLEINQUERY", ds, null, null, inqno, curdate, PostedByid, Posttrmid, PostSession, Posteddat);
                if (!result)
                    return;


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message.ToString()+"');", true);

               

            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqnum = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedItem.Text;
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fdate = this.txtDatefrom.Text.ToString();
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string srchinqno = (inqno.Length > 0 ? inqno : "") + "%";
            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQNO", todate, srchinqno);

            string tdate = "";
            string ToFrDate = "(From :" + fdate + " To " + tdate + ")";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQINFO", ds.Tables[0].Rows[0]["sampleid"].ToString(),
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                // ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt = ds1.Tables[0];


            var lst = new List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();

            foreach (DataRow dr1 in dt.Rows)
            {
                var obj1 = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling();

                obj1.samptype = dr1["samptype"].ToString();
                obj1.samtypedesc = dr1["samtypedesc"].ToString();
                obj1.samqty = Convert.ToDouble(dr1["samqty"]);
                obj1.shoetype = dr1["shoetype"].ToString();
                obj1.agent = dr1["agent"].ToString();
                obj1.agentcod = dr1["agentcod"].ToString();
                obj1.construction = dr1["construction"].ToString();
                obj1.article = dr1["article"].ToString();
                obj1.catagory = dr1["catagory"].ToString();
                obj1.catagorydesc = dr1["catagorydesc"].ToString();
                obj1.comsize = dr1["comsize"].ToString();
                obj1.samsize = dr1["samsize"].ToString();
                obj1.sizerange = dr1["sizerange"].ToString();
                obj1.brand = dr1["brand"].ToString();
                obj1.seasondesc = dr1["seasondesc"].ToString();
                obj1.branddesc = dr1["branddesc"].ToString();
                obj1.season = dr1["season"].ToString();
                obj1.remarks = dr1["remarks"].ToString();


                obj1.lformadesc = dr1["lformadesc"].ToString();

                //string image = obj1.images;
                //obj1.attchmnt = (dr1["attchmnt"].ToString().Trim().Length < 0 ? "" : new Uri(dr1["attchmnt"].ToString()).AbsoluteUri);

                obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;


                lst.Add(obj1);
            }


            LocalReport rpt1 = new LocalReport();



            rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptSampleInterfaceInquery", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(ds1.Tables[0].Rows[0]["deldate"]).ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("sdino", ds1.Tables[0].Rows[0]["inqno"].ToString()));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Sample Development Inquery"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     printType + "', target='_blank');</script>";
        }


        protected void gvSampleInq_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode> lst = (List < SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>) Session["lstgencode"];

            var lstcat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "11");
            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            var lstseason = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "33");
            var lstlformat = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "41");
            var lststype = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "43");
         


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllformaname = (DropDownList)e.Row.FindControl("ddllformaname");
                ddllformaname.DataTextField = "gdesc";
                ddllformaname.DataValueField = "gcod";
                ddllformaname.DataSource = lstlformat;
                ddllformaname.DataBind();
                ddllformaname.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lformacod"));    
                
                DropDownList ddlcatagory = (DropDownList)e.Row.FindControl("ddlcatagory");
                ddlcatagory.DataTextField = "gdesc";
                ddlcatagory.DataValueField = "gcod";
                ddlcatagory.DataSource = lstcat;
                ddlcatagory.DataBind();
                ddlcatagory.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "catagory"));

                DropDownList ddlshoetype = (DropDownList)e.Row.FindControl("ddlshoetype");
                ddlshoetype.DataTextField = "gdesc";
                ddlshoetype.DataValueField = "gcod";
                ddlshoetype.DataSource = lststype;
                ddlshoetype.DataBind();
                ddlshoetype.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "shoetype"));



                DropDownList ddlseason = (DropDownList)e.Row.FindControl("ddlseason");
                ddlseason.DataTextField = "gdesc";
                ddlseason.DataValueField = "gcod";
                ddlseason.DataSource = lstseason;
                ddlseason.DataBind();
                ddlseason.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "season"));
                



                DropDownList ddlagent = (DropDownList)e.Row.FindControl("ddlagent");
                ddlagent.DataTextField = "gdesc";
                ddlagent.DataValueField = "gcod";
                ddlagent.DataSource = lstagent;
                ddlagent.DataBind();
                ddlagent.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "agentcod"));


                //if (e.Row.RowIndex > 0)
                //{
                //    e.Row.FindControl("lbAddMore").Visible = false;
                //}
                //if (this.Request.QueryString["Type"].ToString() != "Approv")
                //{
                //    e.Row.FindControl("LbtnApprove").Visible = false;
                //}


            }

        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            var inqinfo = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string fullPath = System.IO.Path.GetDirectoryName(AsyncFileUpload1.FileName);
            string Url = "";
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string styleid = this.lblStylecode.Text.Trim().ToString();
            string uploadtype = this.ddluploadtype.SelectedValue.ToString();
            if (AsyncFileUpload1.HasFile)
            {

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                //int fileSize = AsyncFileUpload1.PostedFile.ContentLength ;
                string random = ASTUtility.RandNumber(1, 99999).ToString();

                //if (fileSize > 500000)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Image Size Should be Less Than 500KB');", true);
                //    return;
                //}

                //using (Bitmap bmp1 = new Bitmap(fullPath))
                //{
                //    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                //    System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

                //    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                //    EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, 500000);

                //    myEncoderParameters.Param[0] = myEncoderParameter;
                //    bmp1.Save(Server.MapPath("~/Upload/SAMPLE/") + (random + "(1)") + extension, jpgEncoder, myEncoderParameters);

                //}
                //AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);

                //Url = "~/Upload/SAMPLE/" + random + extension;
                ////  Url = Url.Substring(0, (Url.Length - 1));
                ////dt.Rows.Add(comcod, Url);
                //this.Uploadedimg.ImageUrl = Url;

                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    Url = Server.MapPath("~/Upload/SAMPLE/") + random + extension;

                    AsyncFileUpload1.SaveAs(Url);

                    FileInfo fileInfo = new FileInfo(Url);
                    long fileSize = fileInfo.Length;

                    if (fileSize > 102400)
                    {
                        ResizeImage(Url, 220);
                    }

                }

                Url = "~/Upload/SAMPLE/" + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                //dt.Rows.Add(comcod, Url);
                this.Uploadedimg.ImageUrl = Url;

            }


            //  var index = inqinfo.FindIndex(s => s.styleid == styleid);
            if (uploadtype == "samimg")
            {
                inqinfo[0].images = Url;
            }
            else
            {
                inqinfo[0].images = Url;
            }

            ViewState["tblinquery"] = inqinfo;
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "UPDATEPROTOSAMPLEIMAGES", inqno, Url);
            if (result)
            {                //   this.Show_Inquiry_Info();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }
        }





        private void ResizeImage(string Url, int targetSize)
        {
            using (Image image = Image.FromFile(Url))
            {
                int newWidth, newHeight;

                if (image.Width > image.Height)
                {
                    newWidth = targetSize;
                    newHeight = (int)(image.Height * (targetSize / (float)image.Width));
                }
                else
                {
                    newHeight = targetSize;
                    newWidth = (int)(image.Width * (targetSize / (float)image.Height));
                }

                using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                    image.Dispose();
                    File.Delete(Url);
                    newImage.Save(Url);
                }
            }
        }





        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void LbtnDetailsInput_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];

            this.Uploadedimg.ImageUrl = lst[0].images.ToString();
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
     
        protected void lblgvItmCodc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
           // GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
           // int index = row.RowIndex;

            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];
          //  string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");

            ////  string styleid = ((Label)this.gvSampleInq.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();
            //  string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            //  DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            //  if (ds == null || ds.Tables[0].Rows.Count == 0)
            //  {
            //      ((Label)this.Master.FindControl("lblmsg")).Text = "Please Save Iquiry before upload";
            //      return;
            //  }

            //DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ACTUAL_COLOR_SIZE", inqno, styleid, "", "", "", "", "", "", "");
            //ViewState["tblcolor"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //ViewState["tblsizerange"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //ViewState["tblsamsize"] = ds1.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //ViewState["tblconsize"] = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //this.lblStylecode.Text = styleid;

            //var inqinfo = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            //var inqinfo1 = inqinfo.FindAll(c => c.styleid == styleid);
            this.Uploadedimg.ImageUrl = lst[0].images.ToString();
            this.Modal_Data_Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        private void Modal_Data_Bind()

        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];

            var lstupmat = lst.FindAll(l => l.grp == "UP");
            this.gvupper.DataSource = lstupmat;
            this.gvupper.DataBind();


            var lstlimat = lst.FindAll(l => l.grp == "LI");
            this.gvlinem.DataSource = lstlimat;
            this.gvlinem.DataBind();


            var lstskmat = lst.FindAll(l => l.grp == "SK");
            this.gvsockm.DataSource = lstskmat;
            this.gvsockm.DataBind();


            var lstosmat = lst.FindAll(l => l.grp == "OS");
            this.gvosm.DataSource = lstosmat;
            this.gvosm.DataBind();


            var lstacmat = lst.FindAll(l => l.grp == "AC");
            this.gvaccm.DataSource = lstacmat;
            this.gvaccm.DataBind();
        }

        private void Modal_Save_Value()
        {
            //var color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            //for (int i = 0; i < this.gvColor.Rows.Count; i++)
            //{
            //    string colordesc = ((TextBox)gvColor.Rows[i].FindControl("txtColorDesc")).Text.ToString();
            //    string colorselect = (((CheckBox)gvColor.Rows[i].FindControl("gvChkColor1")).Checked == true) ? "Y" : "n";
            //    color[i].colordesc = colordesc;
            //    color[i].colorselect = colorselect;
            //}
          //  ViewState["tblcolor"] = color;
            //----------------------for size range---------------------------------
            //var sizerange = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsizerange"];
            //for (int i = 0; i < this.gvSize.Rows.Count; i++)
            //{
            //    string sizedesc = ((TextBox)gvSize.Rows[i].FindControl("txtgvSizeDesc")).Text.ToString();
            //    string sizeselect = (((CheckBox)gvSize.Rows[i].FindControl("gvChkSize1")).Checked == true) ? "Y" : "n";
            //    sizerange[i].sizedesc = sizedesc;
            //    sizerange[i].sizeselect = sizeselect;
            //}
            //ViewState["tblsizerange"] = sizerange;
            //----------------------for sample size ---------------------------------
            //var samsize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblsamsize"];
            //for (int i = 0; i < this.gvsamsize.Rows.Count; i++)
            //{
            //    string samsizedesc = ((TextBox)gvsamsize.Rows[i].FindControl("txtgvsamSizeDesc")).Text.ToString();
            //    string samsizeselect = (((CheckBox)gvsamsize.Rows[i].FindControl("gvsamChkSize1")).Checked == true) ? "Y" : "n";
            //    samsize[i].sizedesc = samsizedesc;
            //    samsize[i].sizeselect = samsizeselect;
            //}
            //ViewState["tblsamsize"] = samsize;
            //----------------------for Consumption size ---------------------------------
            //var consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
            //for (int i = 0; i < this.gvconsize.Rows.Count; i++)
            //{
            //    string consizedesc = ((TextBox)gvconsize.Rows[i].FindControl("txtgvconSizeDesc")).Text.ToString();
            //    string consizeselect = (((CheckBox)gvconsize.Rows[i].FindControl("gvconChkSize1")).Checked == true) ? "Y" : "n";
            //    consize[i].sizedesc = consizedesc;
            //    consize[i].sizeselect = consizeselect;
            //}
          //  ViewState["tblconsize"] = consize;

        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Modal_Save_Value();
                string comcod = this.GetComCode();
                string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
               // string styleid = lblStylecode.Text.Trim().ToString();
                string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();           

                var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];
                //DataTable dt = ASITUtility03.ListToDataTable(lst);
                //DataSet ds = new DataSet("ds1");
                //ds.Tables.Add(dt);
                // ds.Tables[0].TableName = "tbl1";

                foreach (SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct lst1 in lst)
                {
                    string grp = lst1.grp;
                    string msircode = lst1.msircode;
                    string spcfcod = lst1.spcfcod;

                    bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSORUPDATEPROTOSAMPLEINQUERYMAT", null, null, null, inqno, grp, msircode, spcfcod);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ Merdata.ErrorObject["Msg"].ToString() + "');", true);

                        //((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + Merdata.ErrorObject["Msg"];
                        return;


                    }
                }


                this.Show_Inquiry_Info();
                //  this.ShoeInquiry();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+ ex.Message.ToString() +"');", true);

                
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

       
        protected void lnkbtnDelInquery_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];

           
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComCode();
            int index = 0;
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            
            
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETEINQUERYNUMBER", inqno, "", "");
            if (result)
            {
                lst.RemoveAt(index);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);

              
                return;
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ Merdata.ErrorObject["Msg"].ToString() + "');", true);

             
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

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "APPROVEDSAMINQ", null, null, null, InqNO, userid, AppDat, trmid, sessionid);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Inquery Not Approved');", true);

                   return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Approve Successfully');", true);

           
            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
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

                ((Label)this.Master.FindControl("lblmsg")).Text = "Approve Successfully";
            }
        }

        protected void lnkAddUpperMat_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lstm = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct > lst= (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>) ViewState["tblproinquery"];


            string procode = this.ddlUpperMat.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.ddlUpperMat.SelectedValue.ToString().Substring(12);
            string prodesc = this.ddlUpperMat.SelectedItem.Text.Trim();
            string grp = "UP";
            //  this. prounit= lstm.
            if (lst.FindAll(l => l.msircode == procode && l.grp==grp && l.spcfcod==spcfcod).Count == 0)
            {


                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct
                    { msircode = procode, spcfcod=spcfcod, msirdesc = prodesc, grp = grp, sampleid = 0 };
                lst.Add(objmat);
            
            }
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
            


        }

        protected void lnkAddLineMat_Click(object sender, EventArgs e)
        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lstm = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];


            string procode = this.ddllineMat.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.ddllineMat.SelectedValue.ToString().Substring(12);
            string prodesc = this.ddllineMat.SelectedItem.Text.Trim();
            string grp = "LI";
            //  this. prounit= lstm.
            if (lst.FindAll(l => l.msircode == procode && l.grp == grp && l.spcfcod == spcfcod).Count == 0)
            {


                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct { msircode = procode, spcfcod= spcfcod, msirdesc = prodesc, grp = grp, sampleid = 0 };
                lst.Add(objmat);

            }
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);


        }

        protected void lnkAddSockMat_Click(object sender, EventArgs e)
        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lstm = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];


            string procode = this.ddlsockMat.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.ddlsockMat.SelectedValue.ToString().Substring(12);
            string prodesc = this.ddlsockMat.SelectedItem.Text.Trim();
            string grp = "SK";
            //  this. prounit= lstm.
            if (lst.FindAll(l => l.msircode == procode && l.grp == grp && l.spcfcod == spcfcod).Count == 0)
            {


                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct { msircode = procode, spcfcod=spcfcod, msirdesc = prodesc, grp = grp, sampleid = 0 };
                lst.Add(objmat);

            }
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
        }

        protected void lnkAddOutSoleMat_Click(object sender, EventArgs e)
        {


            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lstm = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];


            string procode = this.ddloutsoleMat.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.ddloutsoleMat.SelectedValue.ToString().Substring(12);
            string prodesc = this.ddloutsoleMat.SelectedItem.Text.Trim();
            string grp = "OS";
            //  this. prounit= lstm.
            if (lst.FindAll(l => l.msircode == procode && l.grp == grp && l.spcfcod == spcfcod).Count == 0)
            {


                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct { msircode = procode, spcfcod=spcfcod, msirdesc = prodesc, grp = grp, sampleid = 0 };
                lst.Add(objmat);

            }
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
        }

        protected void lnkAddAccMat_Click(object sender, EventArgs e)
        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial> lstm = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassMaterial>)Session["lstgetmaterial"];
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];


            string procode = this.ddlaccMat.SelectedValue.ToString().Substring(0,12);
            string spcfcod = this.ddlaccMat.SelectedValue.ToString().Substring(12);
            string prodesc = this.ddlaccMat.SelectedItem.Text.Trim();
            string grp = "AC";
            //  this. prounit= lstm.
            if (lst.FindAll(l => l.msircode == procode && l.grp == grp && l.spcfcod == spcfcod).Count == 0)
            {


                SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct objmat = new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct { msircode = procode, spcfcod=spcfcod, msirdesc = prodesc, grp = grp, sampleid = 0 };
                lst.Add(objmat);

            }
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
        }

        private void DeleteMaterial(string inqno, string grp, string msircode, string spcfcod)

        {

            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];

            string comcod = this.GetComCode();
            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETESAMPLEMAT", null, null, null, inqno, grp, msircode, spcfcod);
            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + Merdata.ErrorObject["Msg"];
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                return;


            }
            lst.RemoveAll(l => l.grp == grp && l.msircode == msircode);
            ViewState["tblproinquery"] = lst;
            this.Modal_Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);



        }

        protected void lnkbtnupup_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string grp = "UP";
            string msircode= ((Label)this.gvupper.Rows[rowindex].FindControl("lblgvmsircodeup")).Text.ToString();
            string spcfcod= ((Label)this.gvupper.Rows[rowindex].FindControl("lblspcfcodup")).Text.ToString();

            this.DeleteMaterial(inqno, grp, msircode, spcfcod);
        }

        protected void lnkbtnline_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string grp = "LI";
            string msircode = ((Label)this.gvlinem.Rows[rowindex].FindControl("lblgvmsircodeline")).Text.ToString();
            string spcfcod = ((Label)this.gvlinem.Rows[rowindex].FindControl("lblspcfcodline")).Text.ToString();
            this.DeleteMaterial(inqno, grp, msircode, spcfcod);
        }

        protected void lnkbtnsock_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string grp = "SK";
            string msircode = ((Label)this.gvsockm.Rows[rowindex].FindControl("lblgvmsircodesock")).Text.ToString();
            string spcfcod = ((Label)this.gvsockm.Rows[rowindex].FindControl("lblspcfcodsock")).Text.ToString();
            this.DeleteMaterial(inqno, grp, msircode, spcfcod);
        }

        protected void lnkbtnsole_Click(object sender, EventArgs e)
        {
            

             int rowindex = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string grp = "OS";
            string msircode = ((Label)this.gvosm.Rows[rowindex].FindControl("lblgvmsircodeosole")).Text.ToString();
            string spcfcod = ((Label)this.gvosm.Rows[rowindex].FindControl("lblspcfcodsole")).Text.ToString();
            this.DeleteMaterial(inqno, grp, msircode, spcfcod);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
        }

        protected void lnkbtnacc_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string grp = "AC";
            string msircode = ((Label)this.gvaccm.Rows[rowindex].FindControl("lblgvmsircodeacc")).Text.ToString();
            string spcfcod = ((Label)this.gvaccm.Rows[rowindex].FindControl("lblspcfcodacc")).Text.ToString();
            this.DeleteMaterial(inqno, grp, msircode, spcfcod);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);

        }

        protected void LbtnDel_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling> lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];


            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComCode();
            int index = 0;
            string inqno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();


            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETEINQUERYNUMBER", inqno, "", "");
            if (result)
            {
                lst.RemoveAt(index);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);


                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Merdata.ErrorObject["Msg"].ToString() + "');", true);


            }

            ViewState["tblinquery"] = lst;
            this.Data_Bind();
        }

        protected void LbtnReRunUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string codetype = this.txtCodeType.Text.ToString()+"%";
            string codedesc = this.TxtNewGenCode.Text.Trim().ToString();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "SAVE_NEW_GENCODE", codetype, codedesc);
            if (result)
            {
                this.TxtNewGenCode.Text = String.Empty;
                this.GetGenCode();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('New Code Saved Successfully');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

            }
        }

        protected void LbtnArticleHistory_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();

            string article = this.TxtArticle.Text.Trim().ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GET_ARTICLE_NAME_HISTORY", article);
            if (ds1 == null)
            {
                return;
            }
            this.GvArticleHistory.DataSource = ds1.Tables[0];
            this.GvArticleHistory.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DrawerModal();", true);

        }
    }

}