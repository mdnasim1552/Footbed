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
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;
using SPEENTITY;
using SPELIB;
using SPEENTITY.C_09_Commer;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_09_Commer
{
    public partial class LCOpening : System.Web.UI.Page
    {
        ProcessAccess proc1 = new ProcessAccess();
        static string prevPage = String.Empty;
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Open") ? "L/C Openning" :
                    (Request.QueryString["Type"].ToString() == "receive") ? "Foreign Material Recived" : "L/C Costing";   //=



              //  ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
             


                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approve";

                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                this.CurrencyInf();
                GetLcType();
                this.GetOther();
                imgbtnLcsearch_Click(null, null);

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //(LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(btnNew_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            // ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnReCalculate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).CheckedChanged += new EventHandler(chkPayment_CheckedChanged);
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable tbl4 = (DataTable)ViewState["TblOrder"];
            DataTable dt = (DataTable)ViewState["tblLcinfo"];
            var lst = tbl4.DataTableToList<BO_LCOpening>();
            var lst1 = dt.DataTableToList<BO_LCGenInfo>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptLcInfowithReq", lst, lst1, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string reqno = this.Request.QueryString["genno"].ToString();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataSet ds = proc1.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "APPROVE_LC_REQ", null, null, null, reqno, actcode, userid, Sessionid, Terminal, Posteddat);


            if (ds == null)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ds.Tables[0].Rows[0]["msg"].ToString() +"');", true);

     



        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected string ComCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        protected void imgbtnLcsearch_Click(object sender, EventArgs e)
        {

            string comcod = this.ComCod();
            string SlcNO = "%%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();

            ViewState["tblStoreType"] = ds1.Tables[0];
            if (this.Request.QueryString["actcode"].ToString()!= "000000000000")
            {
                this.ddlLcCode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                if (this.Request.QueryString["actcode"].ToString() != "")
                {
                    this.lnkOpen_Click(null, null);
                }
                
            }
        }

        protected void lnkbtnSaveCust_Click(object sender, EventArgs e)
        {

            this.detailsinfo();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblLcinfo"];
            string Gvalue = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();

                if (Gcode == "15001" || Gcode == "15003" || Gcode == "15008" || Gcode == "15016" || Gcode == "15030" || Gcode == "15033" || Gcode == "15035" || Gcode == "15038" || Gcode == "15054" || Gcode == "15055" || Gcode == "15075" || Gcode == "15080" || Gcode == "15087" || Gcode == "15096" || Gcode == "15109" || Gcode == "15091" ||
                      Gcode == "15095" || Gcode == "15098" || Gcode == "15057")

                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "15021" || Gcode == "15022" || Gcode == "15023" || Gcode == "15024" || Gcode == "15026" ||
                          Gcode == "15110" || Gcode == "15111" || Gcode == "15090" || Gcode == "15093" || Gcode == "15093"
                         || Gcode == "15027" || Gcode == "15107" || Gcode == "15020" || Gcode == "15018" || Gcode == "15025"
                         || Gcode == "15050" || Gcode == "15065" || Gcode == "15100" || Gcode == "15105")
                {

                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                        : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).SelectedValue.ToString();
                }
                else if (Gcode == "15010")
                {
                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Count == 0) ?
                      ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).SelectedValue.ToString();

                }
                else if (Gcode == "15060")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() == "") ? "0.00" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                }
                else
                {
                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Count == 0) ?
                       ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).SelectedValue.ToString();
                }

                dt.Rows[i]["gdesc1"] = Gvalue;
            }



            ViewState["tblLcinfo"] = dt;

        }


        protected void detailsinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ((DataTable)ViewState["tblLcinfo"]).Copy();
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_LC_INFO", "INSERTORUPDATELCINF", ds1, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

           

        }
        private void GetOther()
        {
            string comcod = this.ComCod();
            //ViewState.Remove("tblcur");
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETCURRENCYAGST", "", "", "", "", "", "", "", "", "");
            ViewState["tblSup"] = ds1.Tables[0];
            ViewState["tblBank"] = ds1.Tables[1];
            ViewState["tblOther"] = ds1.Tables[2];
            ViewState["tblLcdata"] = ds1.Tables[3];
            ViewState["tblCountry"] = ds1.Tables[4];
            ds1.Dispose();
        }
        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            if (ds == null)
                return;
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();

            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;

        }
        protected void lnkOpen_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //this.LoadAcccombo();
            //this.CurrencyInf();
            //this.SupplierInfo();
            this.ResourceCode();
            if (this.lnkOpen.Text == "Ok")
            {
                this.LbtnCopy.Visible = true;
                this.ddlFromCopy.Visible = true;
                this.Lblfrom.Visible = true;
                this.ddlSupplier.Visible = true;
                this.LbSupp.Visible = true;
                this.lnkOpen.Text = "New";

                //--------------------------Order,Receive,Costing-----------//
                string criteria = Request.QueryString["Type"].ToString();
                switch (criteria)
                {
                    case "Open":
                        this.GetGenInfo();
                        this.CallOrder();
                        break;


                }
                this.ddlLcCode.Enabled = false;
                string lccod = this.ddlLcCode.SelectedValue.ToString();
                DataTable dt = (DataTable)ViewState["tblStoreType"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "actcode <>'" + lccod + "'";

                this.ddlFromCopy.DataTextField = "actdesc";
                this.ddlFromCopy.DataValueField = "actcode";
                this.ddlFromCopy.DataSource = dv.ToTable();
                this.ddlFromCopy.DataBind();


                if (this.Request.QueryString["genno"].Length == 0)
                {
                    this.lbtnReq.Visible = false;
                    this.ddlSupplier.Visible = false;
                }
                else
                {
                    this.ddlSupplier.Visible = true;
                    this.lbtnReq.Visible = true;
                    string comcod = this.ComCod();
                    //ViewState.Remove("tblcur");
                    //DataSet dssupplier = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_REQWISE_SUPPLIER", this.Request.QueryString ["genno"].ToString(), "", "", "", "", "", "", "", "");
                    //if (dssupplier == null || dssupplier.Tables [0].Rows.Count == 0)
                    //    return;
                    DataTable dtsuplier = (DataTable)ViewState["tblSup"];
                    this.ddlSupplier.DataTextField = "gdesc";
                    this.ddlSupplier.DataValueField = "gcod";
                    this.ddlSupplier.DataSource = dtsuplier;
                    this.ddlSupplier.DataBind();
                    if (this.Request.QueryString["ssircode"].Length > 0)
                    {
                        this.ddlSupplier.SelectedValue = this.Request.QueryString["ssircode"].ToString();
                    }
                }

                //--------------------------------------//

            }
            else
            {
                this.ddlSupplier.Visible = false;
                this.LbtnCopy.Visible = false;
                this.ddlFromCopy.Visible = false;
                this.Lblfrom.Visible = false;
                this.lnkOpen.Text = "Ok";
                this.MultiView1.ActiveViewIndex = -1;
                this.ddlLcCode.Enabled = true;
                this.lbtnReq.Visible = false;
                this.gvPersonalInfo.DataSource = null;
                this.gvPersonalInfo.DataBind();
            }
        }
        private void GetGenInfo()
        {
            string comcod = this.ComCod();
            string ActCode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "CUSTERSONALINFO", ActCode, "", "", "", "", "", "", "", "");
            ViewState["tblLcinfo"] = ds1.Tables[0];

            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            DataTable dt = ds1.Tables[0];
            DataTable dtsup = (DataTable)ViewState["tblSup"];
            DataTable dtBank = (DataTable)ViewState["tblBank"];
            DataTable dtOther = (DataTable)ViewState["tblOther"];
            DataTable tblLcdata = (DataTable)ViewState["tblLcdata"];
            DataTable tblCountry = (DataTable)ViewState["tblCountry"];



            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst12 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
            var lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>)ViewState["tblcurdesc"];


            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {


                    case "15021": //Name of L/C Opening Bank
                        DataTable dtOpbnk = tblLcdata.Copy();
                        DataView dv21 = dtOpbnk.DefaultView;
                        dv21.RowFilter = ("gcod like '010100401%'");

                        DataRowView newRow = dv21.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv21.ToTable().Rows.Add(newRow);
                        dv21.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv21.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15022": //Address of L/C Opening Bank


                        DataTable dtAddress = tblLcdata.Copy();


                        DataView dv22 = new DataView(dtAddress);
                        dv22.RowFilter = ("gcod like '010100402%'");


                        newRow = dv22.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv22.ToTable().Rows.Add(newRow);
                        dv22.Sort = "gcod desc";



                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv22.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15023": //Swift Code
                        DataTable dtSwift = tblLcdata.Copy();
                        DataView dv23 = dtSwift.DefaultView;
                        dv23.RowFilter = ("gcod like '010100403%'");

                        newRow = dv23.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv23.ToTable().Rows.Add(newRow);
                        dv23.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv23.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15026": //Swift Code
                        DataTable dtSwifts = tblLcdata.Copy();
                        DataView dv26 = dtSwifts.DefaultView;
                        dv26.RowFilter = ("gcod like '010100406%'");

                        newRow = dv26.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv26.ToTable().Rows.Add(newRow);
                        dv26.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv26.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;
                    case "15024": //Name of L/C Advising Bank
                        DataTable dtAb = tblLcdata.Copy();
                        DataView dv24 = dtAb.DefaultView;
                        dv24.RowFilter = ("gcod like '010100404%'");


                        newRow = dv24.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv24.ToTable().Rows.Add(newRow);
                        dv24.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv24.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15027": //Address of L/C Advising Bank
                        DataTable dtaab = tblLcdata.Copy();
                        DataView dv27 = dtaab.DefaultView;
                        dv27.RowFilter = ("gcod like '010100405%'");

                        newRow = dv27.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv27.ToTable().Rows.Add(newRow);
                        dv27.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv27.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15020": //Beneficiary's Country
                        DataTable dtbc = tblCountry.Copy();
                        DataView dv20 = dtbc.DefaultView;
                        //  dv20.RowFilter = ("gcod like '010100408%'");

                        newRow = dv20.AddNew();
                        newRow["id"] = "";
                        newRow["nicename"] = "--------Select--------";
                        dv20.ToTable().Rows.Add(newRow);
                        dv20.Sort = "id desc";



                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "nicename";
                        ddlgval.DataValueField = "id";
                        ddlgval.DataSource = dv20.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";




                        break;











                    case "15107": //Country of Origin of Goods
                        DataTable dtcg = tblCountry.Copy();
                        DataView dv07 = dtcg.DefaultView;
                        //dv07.RowFilter = ("gcod like '010100409%'");

                        newRow = dv07.AddNew();
                        newRow["id"] = "";
                        newRow["nicename"] = "--------Select--------";
                        dv07.ToTable().Rows.Add(newRow);
                        dv07.Sort = "id desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "nicename";
                        ddlgval.DataValueField = "id";
                        ddlgval.DataSource = dv07.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;
                    case "15110": //Port of Loading
                        DataTable dtpl = tblLcdata.Copy();
                        DataView dv10 = dtpl.DefaultView;
                        dv10.RowFilter = ("gcod like '010100411%'");


                        newRow = dv10.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv10.ToTable().Rows.Add(newRow);
                        dv10.Sort = "gcod desc";

                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv10.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15111": //Port of Discharge
                        DataTable dtpd = tblLcdata.Copy();
                        DataView dv11 = dtpd.DefaultView;
                        dv11.RowFilter = ("gcod like '010100412%'");
                        newRow = dv11.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv11.ToTable().Rows.Add(newRow);
                        dv11.Sort = "gcod desc";



                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv11.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;


                    case "15090": //Document Status
                        DataTable dtds = tblLcdata.Copy();
                        DataView dv90 = dtds.DefaultView;
                        dv90.RowFilter = ("gcod like '010100415%'");


                        newRow = dv90.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv90.ToTable().Rows.Add(newRow);
                        dv90.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv90.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15093": //Name of C & F
                        DataTable dtcf = tblLcdata.Copy();
                        DataView dv93 = dtcf.DefaultView;
                        dv93.RowFilter = ("gcod like '990102001%'");


                        newRow = dv93.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv93.ToTable().Rows.Add(newRow);
                        dv93.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv93.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;




                    case "15010": //Currency
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency"));
                        ddlgval.DataTextField = "curdesc";
                        ddlgval.DataValueField = "curcode";
                        ddlgval.DataSource = lst1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        this.ddlcurrency_SelectedIndexChanged(null, null);
                        break;

                    case "15025": //Supplier

                        DataTable dtsupa = dtsup.Copy();
                        DataView dv25 = dtsupa.DefaultView;
                        newRow = dv25.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv25.ToTable().Rows.Add(newRow);
                        dv25.Sort = "gcod desc";


                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv25;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15018": //Bank
                        DataTable dtBankz = dtBank.Copy();
                        DataView dv18 = dtBankz.DefaultView;
                        newRow = dv18.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv18.ToTable().Rows.Add(newRow);
                        dv18.Sort = "gcod desc";



                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv18;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;
                    case "15050": //INSURANCE COMPANY
                        DataTable dttc = tblLcdata.Copy();
                        DataView dv50 = dttc.DefaultView;
                        dv50.RowFilter = ("gcod like '999900130%'");

                        newRow = dv50.AddNew();
                        newRow["gcod"] = "";
                        newRow["gdesc"] = "--------Select--------";
                        dv50.ToTable().Rows.Add(newRow);
                        dv50.Sort = "gcod desc";




                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv50.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;

                    case "15065": //L/C Payment Terms
                        DataTable dtpayTr = dtOther.Copy();
                        DataView dv = dtpayTr.DefaultView;
                        dv.RowFilter = ("gcod like '12%'");
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc1";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;
                    case "15100": //Delivery Terms
                        DataTable dtDelTr = dtOther.Copy();
                        DataView dv1 = dtDelTr.DefaultView;
                        dv1.RowFilter = ("gcod like '13%'");
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc1";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;
                    case "15105": //Delivery Mode
                        DataTable dtDelMod = dtOther.Copy();
                        DataView dv2 = dtDelMod.DefaultView;
                        dv2.RowFilter = ("gcod like '14%'");
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc1";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:200px;";
                        break;



                    case "15001": //Date Time 
                    case "15003":
                    case "15008":

                    case "15016":
                    case "15030":
                    case "15033":
                    case "15035":
                    case "15038":
                    case "15054":
                    case "15055":

                    case "15075": //B/L, AWB/D/N Date
                    case "15080": //ETA Date
                    case "15087": //B/E Date
                    case "15096": //Received at Factory

                    case "15109":
                    case "15091":
                    case "15095":
                    case "15098":
                    case "15057":

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Visible = false;

                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Visible = false;
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Visible = false;

                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Visible = false;
                        break;

                }

            }
        }
        protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["tblLcinfo"];
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("gcod='15011'");
                dt1 = dv.ToTable();

                string Rate = dt1.Rows[0]["gdesc1"].ToString();
                if (Rate.Length != 0)
                {
                    this.txtconv.Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
                }
                var lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
                if (lst1 == null)
                    return;
                string fcode = "001";
                for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();


                    switch (Gcode)
                    {
                        case "15010":
                            string tcode = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).SelectedValue.ToString();

                            if (Rate.Length != 0)
                            {
                                ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
                                this.txtconv.Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
                            }
                            else
                            {
                                ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                                this.txtconv.Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                            }

                            break;

                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        private void CallOrder()
        {

            string tname = Request.QueryString["Type"].ToString();

            try
            {

                if (tname == "Open")
                {
                    this.Panel3.Visible = true;
                }
                this.MultiView1.ActiveViewIndex = 0;
                this.showOrder();
                LoadOrderDgv();

            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message+"');", true);

                
            }


        }
        private void showOrder()
        {
            ViewState.Remove("TblOrder");
            string comcod = this.ComCod();
            string lcno1 = this.ddlLcCode.SelectedValue.ToString().Trim();
            string ordrid = "";
            //string suplier=this.ddlSupplier.SelectedValue.ToString();
            DataSet ds5 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVELCINFO2", lcno1, ordrid, "LCINFO2", "", "", "", "", "", ""); //table Desc3
            ViewState["TblOrder"] = ds5.Tables[0];
            ViewState["Tblcharging"] = ds5.Tables[1];
            LoadOrderDgv();
            //Calculation();
        }
        protected void lnkAddTable_Click(object sender, EventArgs e)
        {
            if (ViewState["TblOrder"] == null)
            {
                DataTable tbl2 = new DataTable();
                tbl2.Columns.Add("rescod", Type.GetType("System.String"));
                tbl2.Columns.Add("resdesc", Type.GetType("System.String"));
                tbl2.Columns.Add("spcfcode", Type.GetType("System.String"));
                tbl2.Columns.Add("spcfdesc", Type.GetType("System.String"));
                tbl2.Columns.Add("scode", Type.GetType("System.String"));
                tbl2.Columns.Add("unit", Type.GetType("System.String"));
                tbl2.Columns.Add("ordrqty", Type.GetType("System.Double"));
                tbl2.Columns.Add("freeqty", Type.GetType("System.Double"));
                tbl2.Columns.Add("rate", Type.GetType("System.Double"));
                tbl2.Columns.Add("amount", Type.GetType("System.Double"));
                tbl2.Columns.Add("bdamount", Type.GetType("System.Double"));
                tbl2.Columns.Add("ssircode", Type.GetType("System.String"));
                tbl2.Columns.Add("reqno", Type.GetType("System.String"));
                tbl2.Columns.Add("bomid", Type.GetType("System.String"));
                tbl2.Columns.Add("syspon", Type.GetType("System.String"));
                ViewState["TblOrder"] = tbl2;
            }
            double ConAmt = Convert.ToDouble(this.txtconv.Text);



            DataTable tbl3 = (DataTable)ViewState["TblOrder"];
            DataTable tblr = (DataTable)ViewState["Material"];

            string rescode = this.ddlResList.SelectedValue.Trim().ToString();
            string spcfcode = this.ddlResSpcf.SelectedValue.ToString();
            string supcode = this.ddlSupplier.SelectedValue.ToString();
            //  foreach (string rescode in arrbilno)
            //    {

            DataRow[] dr2 = tbl3.Select("rescod='" + rescode + "' AND spcfcode='" + spcfcode + "'");
            if (dr2.Length == 0)
            {
                DataRow[] drb = tblr.Select("rescod='" + rescode + "'");

                DataRow dr1 = tbl3.NewRow();
                dr1["rescod"] = rescode;
                dr1["resdesc"] = (((DataTable)ViewState["Material"]).Select("rescod='" + rescode + "'"))[0]["resdesc"].ToString();
                dr1["spcfcode"] = spcfcode;
                dr1["ssircode"] = supcode;
                dr1["reqno"] = "00000000000000";
                dr1["bomid"] = "0000000000";
                dr1["syspon"] = "0000000000";
                if (spcfcode == "000000000000")
                {
                    dr1["spcfdesc"] = "";

                }
                else
                {
                    dr1["spcfdesc"] = (((DataTable)ViewState["tblSpcf"]).Select("rsircode='" + rescode + "' and spcfcod='" + spcfcode + "'"))[0]["spcfdesc"].ToString();
                }

                dr1["scode"] = drb[0]["resdesc3"];
                dr1["unit"] = drb[0]["sirunit"];
                dr1["ordrqty"] = 0;
                dr1["freeqty"] = 0;
                dr1["rate"] = Convert.ToDouble(drb[0]["sirval"]) / ConAmt;
                dr1["amount"] = 0;
                dr1["bdamount"] = 0;
                tbl3.Rows.Add(dr1);

            }
            //   }

            ViewState["TblOrder"] = tbl3;
            LoadOrderDgv();

        }
        private void Footcal()
        {
            DataTable dt1 = (DataTable)ViewState["TblOrder"];
            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFOrderQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFFreeqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(freeqty)", "")) ? 0.00 : dt1.Compute("sum(freeqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amount)", "")) ? 0.00 : dt1.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFBDTamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bdamount)", "")) ? 0.00 : dt1.Compute("sum(bdamount)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        private void LoadOrderDgv()
        {
            try
            {
                DataTable tbl4 = (DataTable)ViewState["TblOrder"];
                DataTable tblcharging = (DataTable)ViewState["Tblcharging"];

                if (tbl4.Rows.Count <= 0)
                {
                    this.dgvOrder.DataSource = null;
                    this.dgvOrder.DataBind();
                    this.gvcharging.DataSource = null;
                    this.gvcharging.DataBind();
                    this.lblcharging.Visible = false;
                    return;

                }
                //Calculation();
                this.lblcharging.Visible = true;
                this.dgvOrder.DataSource = tbl4;
                this.dgvOrder.DataBind();
                this.gvcharging.DataSource = tblcharging;
                this.gvcharging.DataBind();

                Session["Report1"] = dgvOrder;
                ((HyperLink)this.dgvOrder.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                this.Footcal();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message+"');", true);

              
            }

        }
        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.Calculation();
            this.LoadOrderDgv();
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
        }
        private void Calculation()
        {
            try
            {
                DataTable tbl4 = (DataTable)ViewState["TblOrder"];

                double Tamt = 0.00;
                double BDTamt = 0.00;
                for (int i = 0; i < dgvOrder.Rows.Count; i++)
                {
                    double Orderqty = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Trim());
                    double Freeqty = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvFreeqty")).Text.Trim());
                    double Rate = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text.Trim());
                    double Amount = Orderqty * Rate;
                    Tamt += Amount;
                    double BDTAmount = Amount * Convert.ToDouble(this.txtconv.Text); //().trim().ToString("#,##0.00;(#,##0.00); ");
                    BDTamt += BDTAmount;
                    ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text = Orderqty.ToString("#,##0.00;(#,##0.00); ");
                    ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text = Rate.ToString("#,##0.000000;(#,##0.000000); ");
                    ((Label)dgvOrder.Rows[i].FindControl("lblgrvamount")).Text = Amount.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)dgvOrder.Rows[i].FindControl("lblgrvBDTamount")).Text = BDTAmount.ToString("#,##0.00;(#,##0.00); ");



                    tbl4.Rows[i]["ordrqty"] = Orderqty;
                    tbl4.Rows[i]["freeqty"] = Freeqty;
                    tbl4.Rows[i]["rate"] = Rate;
                    tbl4.Rows[i]["amount"] = Amount;
                    tbl4.Rows[i]["bdamount"] = BDTAmount;
                }
                ViewState["TblOrder"] = tbl4;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:There is no Order!!');", true);
               
            }
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)    // fn up order
        {

            string comcod = this.ComCod();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            this.Calculation();
            //string orderid = this.Request.QueryString["genno"].ToString();


            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                string syspon = ((Label)dgvOrder.Rows[i].FindControl("lblgvsyspon")).Text.ToString();
                string rescode = ((Label)dgvOrder.Rows[i].FindControl("lblgvResCode")).Text.Replace("-", "");
                string bomid = ((Label)dgvOrder.Rows[i].FindControl("lblBomid")).Text.ToString();
                string spcfcode = ((Label)dgvOrder.Rows[i].FindControl("lblgrvspccode")).Text.ToString();
                string ssircode = ((Label)dgvOrder.Rows[i].FindControl("lblgvssircode")).Text.ToString();
                string orderid = ((Label)dgvOrder.Rows[i].FindControl("lblgvreqno")).Text.ToString();

                string ordrqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Replace(",", ""))).ToString();
                string Freeqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvFreeqty")).Text.Replace(",", ""))).ToString();
                string rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text.Replace(",", ""))).ToString();
                DataSet ds4 = proc1.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "LCINFO2_UPDATE", null, null, null, actcode, rescode, ordrqty, rate, orderid, Freeqty, spcfcode, bomid, syspon, ssircode); //table Desc 6
                if (ds4 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + proc1.ErrorObject["Msg"].ToString()+"');", true);
                              

                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Record Update Successfully');", true);



            }

        }
        protected void dgvOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            string comcod = this.ComCod();
            DataTable dt = (DataTable)ViewState["TblOrder"];
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string rescode = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblgvResCode")).Text.Trim();
            string spcfcode = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblgrvspccode")).Text.Trim();
            string reqno = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblgvreqno")).Text.Trim();
            string bomid = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblBomid")).Text.Trim();
            string syspon = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblgvsyspon")).Text.Trim();

            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "DELETELCMAT", lcno2, rescode, spcfcode, reqno, bomid, syspon);
            string action = result.Tables[0].Rows[0]["msg"].ToString();

            if (action == "Sucess")
            {
                int RowIndex = dgvOrder.PageSize * dgvOrder.PageIndex + e.RowIndex;

                try
                {
                    if (dt == null)
                    {
                        return;
                    }

                    dt.Rows[RowIndex].Delete();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);

                  
                    ViewState["TblOrder"] = dt;
                    this.LoadOrderDgv();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message+ "');", true);

                 
                }



            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ action + "');", true);

             

            }
        }
        class SumClass
        {
            public string Product_Id { get; set; }
            public double rcvqty { get; set; }

        }
        private void ResourceCode()
        {
            string comcod = this.ComCod();
            string filter1 = "%%";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            //string LcCode1 = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 8);
            string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string LcCode = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 4);
            //LcCode = (LcCode == "1401") ? LcCode1 : LcCode;


            DataTable dt = (DataTable)ViewState["tblStoreType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + actcode + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["code"].ToString();
            string SearchInfo = "";
            if (Codetype.Length > 0)
            {

                int len;
                string[] ar = Codetype.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }

            SearchInfo = (SearchInfo.Length == 0) ? ((LcCode == "1401") ? "sircode like '04%'"
            : (LcCode == "1402") ? "sircode like '03%'"
            : (LcCode == "1403") ? "sircode like '21%'" : "sircode like '04%' or sircode like '03%' or sircode like '21%'") : SearchInfo;

            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", "000000000000", curdate, "%", SearchInfo, "", "", "", "", "");

            this.ddlResList.DataTextField = "resdesc1";
            this.ddlResList.DataValueField = "rescod";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
            ViewState["Material"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];
            ImgbtnSpecification_Click(null, null);
        }
        protected void lnkSameValue_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            LoadOrderDgv();
        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["TblOrder"];

            int RowIndex = 0;

            for (int i = 0; i < this.dgvOrder.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Trim());
                RowIndex = this.dgvOrder.PageIndex * this.dgvOrder.PageSize + i;
                dt.Rows[RowIndex]["ordrqty"] = Qty;
            }

            ViewState["TblOrder"] = dt;
        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SIRINF_MAT_SPCF_LIST", mResCode, "", "", "", "");

            if(ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Specification Found For This Item');", true);
                return;
            }
            
            if(ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Specification Found For This Item');", true);
                return;
            }

            this.ddlResSpcf.Items.Clear();
            ViewState["tblSpcf"] = ds1.Tables[0];
            
            //if (ds1.Tables[0].Rows.Count > 1)
            //{
            //    ds1.Tables[0].Rows[0].Delete();
            //}

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = ds1.Tables[0];
            this.ddlResSpcf.DataBind();
        }
        protected void lbtnReq_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string dayid =(this.gvCheckbx.Checked==true)? this.ddlPoNo.SelectedValue.ToString(): this.Request.QueryString["dayid"].ToString();


            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GET_REQWISE_MATERIAL_LIST", dayid, "");
            if (ds1 == null)
            {
                return;
            }
            ViewState["tblReqMatlist"] = ds1.Tables[0];
            this.gvMatlist.DataSource = ds1.Tables[0];
            this.gvMatlist.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenMatlistmodal();", true);

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string lcNumber = this.TxtLCnumber.Text.Trim().ToString();
            string lchead = ASTUtility.Left(this.ddlLcCategoris.SelectedValue.ToString(), 8) + "%";//this.txtlchead.Text.Trim().ToString() + "%";

            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_LC_INFO", "SAVE_LC_FROM_LC_OPENING", lcNumber, lchead);
            if (result)
            {
                this.imgbtnLcsearch_Click(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Save Successfully');", true);

               
            }
        }
        protected void btnFiledvalueentru_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).NamingContainer;

            Label lblcode = (Label)row.FindControl("lblgvItmCode");
            Label lgcResDesc1 = (Label)row.FindControl("lgcResDesc1");


            this.lcFiled.Text = lgcResDesc1.Text.Trim();
            this.lblgcod.Text = lblcode.Text.Trim();



            GetLastCode();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        private void GetLastCode()
        {
            string comcod = this.ComCod();
            string gocd = lblgcod.Text.Trim().ToString();

            DataSet ds = proc1.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "GET_LAST_CODE", null, null, null, gocd);

            if (ds == null)
                return;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLcType()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETLCTYPE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {

                DataTable dtlcCategr = ds2.Tables[0].Copy();
                DataView dv21 = dtlcCategr.DefaultView;
                dv21.RowFilter = ("actcode <> '000000000000'");

                this.ddlLcCategoris.DataSource = dv21.ToTable();
                this.ddlLcCategoris.DataTextField = "actdesc";
                this.ddlLcCategoris.DataValueField = "actcode";
                this.ddlLcCategoris.DataBind();

                ds2.Dispose();

            }
        }
        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string lccod = this.ddlLcCode.SelectedValue.ToString();
            string fromlc = this.ddlFromCopy.SelectedValue.ToString();
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_LC_INFO", "COPY_LC_GENERALINF", lccod, fromlc);

            if (result == true)
            {
                this.GetGenInfo();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Copied Successfully');", true);

                return;
            }



        }
        protected void lblgvSpfDesc10_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            this.ModalHead.Text = "Material Specification Change";
            string sircode = ((Label)this.dgvOrder.Rows[rowindex].FindControl("lblgvResCode")).Text.Trim();
            string spcfcod = ((Label)this.dgvOrder.Rows[rowindex].FindControl("lblgrvspccode")).Text.Trim();
            string bomid = ((Label)this.dgvOrder.Rows[rowindex].FindControl("lblBomid")).Text.Trim();
            this.lblHelper.Text = sircode + spcfcod + bomid;
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", sircode);
            DataSet colorinfo = this.proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            ViewState["tblcolor"] = colorinfo.Tables[0];
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Found Specification');", true);

                
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            this.TxtThikness.Text = "";
            this.txtlength.Text = "";
            this.txtbrand.Text = "";
            this.txtRemarks.Text = "";
            this.ddlModalColor.DataTextField = "gdesc";
            this.ddlModalColor.DataValueField = "gcod";
            this.ddlModalColor.DataSource = colorinfo;
            this.ddlModalColor.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SpcfChangModal();", true);
        }
        protected void LbtnChngSpcf_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblcolor"];
            DataView dv = dt.DefaultView;
            DataTable dt2 = (DataTable)ViewState["TblOrder"];
            string material = this.lblHelper.Text.ToString().Substring(0, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(12, 12);
            string bomid = this.lblHelper.Text.ToString().Substring(24, 10);
            string reqno = dt2.Rows[0]["reqno"].ToString();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            if (this.txtlength.Text.ToString() != "" || this.TxtThikness.Text.ToString() != "")
            {
                string thikness = this.TxtThikness.Text.ToString();
                string len = this.txtlength.Text.ToString();
                string brand = this.txtbrand.Text.ToString();
                string remarks = this.txtRemarks.Text.ToString();
                string newcolor = this.ddlModalColor.SelectedValue.ToString();
                string newcolorname = this.ddlModalColor.SelectedItem.ToString();
                dv.RowFilter = "gcod = '" + newcolor + "'";
                string colcode = dv.ToTable().Rows[0]["colcode"].ToString();

                DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_NEW_SPECIFICATIONS", material, thikness, len, newcolorname, brand, remarks, colcode);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Error!!!');", true);


                    return;
                }

                tospcfcod = ds.Tables[0].Rows[0]["spcfcod"].ToString();
            }
            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "CHANGE_SPECIFICATIONS_OF_LCOPENING", actcode, reqno, material, spcfcode, tospcfcod, bomid);
            if (result == true)
            {
                this.CallOrder();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Specification Change Successfully');", true);


            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist This Specifications');", true);

              
            }
        }
        protected void ModalUpdateBtnLink_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
           // string reqno = this.Request.QueryString["genno"].ToString();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            //string ssircode = this.ddlSupplier.SelectedValue.ToString();
            for (int j = 0; j < gvMatlist.Rows.Count; j++)
            {
                if (((CheckBox)this.gvMatlist.Rows[j].FindControl("chkack")).Checked == true && ((CheckBox)this.gvMatlist.Rows[j].FindControl("chkack")).Enabled == true)
                {
                    string bomid = Convert.ToString(((Label)this.gvMatlist.Rows[j].FindControl("lblgvorderno")).Text.Trim());
                    string rsircode = Convert.ToString(((Label)this.gvMatlist.Rows[j].FindControl("lblrsircode")).Text.Trim());
                    string spcfcod = Convert.ToString(((Label)this.gvMatlist.Rows[j].FindControl("lblspcfcod")).Text.Trim());
                    string ssircode = Convert.ToString(((Label)this.gvMatlist.Rows[j].FindControl("lblssircode")).Text.Trim());
                    double ReqQty = Convert.ToDouble("0" + ((Label)this.gvMatlist.Rows[j].FindControl("lblgvRecvqty")).Text.Trim());
                    double ReqRate = Convert.ToDouble("0" + ((Label)this.gvMatlist.Rows[j].FindControl("lblgvRate")).Text.Trim());
                    string reqno =((Label)this.gvMatlist.Rows[j].FindControl("lblgvReqno")).Text.Trim().ToString();
                    string syspon = ((Label)this.gvMatlist.Rows[j].FindControl("lblgvSysPono")).Text.Trim().ToString();
                    double balqty = Convert.ToDouble("0" + ((Label)this.gvMatlist.Rows[j].FindControl("lblgvBalQty")).Text.Trim());



                    DataSet ds = proc1.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "UPDATE_LC_WITH_REQ",
                        null, null, null, reqno, actcode, ssircode, rsircode, spcfcod, bomid, balqty.ToString(), ReqRate.ToString(), syspon);

                    if (ds == null)
                        return;

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ds.Tables[0].Rows[0]["msg"].ToString()+"');", true);

                   
                   

                }

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                  

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

              



            }
            this.showOrder();
            LoadOrderDgv();
        }

        protected void gvCheckbx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.gvCheckbx.Checked == true)
            {
                string comcod = this.GetCompCode();
                string prPoNo = this.Request.QueryString["dayid"].ToString();
                string supcode = this.ddlSupplier.SelectedValue.ToString();
                DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "GETREMPO", prPoNo, supcode);

                this.ddlPoNo.DataTextField = "txtfield";
                this.ddlPoNo.DataValueField = "valfield";
                this.ddlPoNo.DataSource = ds.Tables[0];
                this.ddlPoNo.DataBind();

                this.ddlPoNo.Visible = true;
            }
            else
            {
                this.ddlPoNo.Visible = false;
            }
        }
    }
}