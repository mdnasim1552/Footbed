using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SPELIB;
using SPERDLC;
using Microsoft.Reporting.WinForms;


namespace SPEWEB.F_19_EXP
{
    public partial class DelvChallan : System.Web.UI.Page
    {

        ProcessAccess proc1 = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetBuyer();
               
                this.ShowShiplineType();

                this.ShowOtherInfo();
                ((Label)this.Master.FindControl("lblTitle")).Text = " Delivery Challan";


                this.CommonButton();

                if (this.Request.QueryString["actcode"].Length != 0)
                {

                    if (this.Request.QueryString["Type"].ToString() == "Approve" || this.Request.QueryString["Type"].ToString() == "Edit")
                    {
                        this.PreviousList();
                        this.ddlPrevList.SelectedValue = this.Request.QueryString["sircode"].ToString();
                        this.lbtnOk_Click(null, null);
                    }
                    else
                    {
                        this.plnexport.Visible = false;
                        this.lbtnOk_Click(null, null);
                    }
                }
                this.Loadtrans();
            }

            string comcod = this.GetComCode();

            if(comcod == "5305" || comcod == "5306") 
            {
                this.txtCntnrNo.Visible = true;
                this.txtLicnseNo.Visible = true;
            }
            else
            {
                
                this.lblContainer.Visible = false;
                this.txtCntnrNo.Visible = false;
                
                this.lblLicnseNo.Visible = false;
                this.txtLicnseNo.Visible = false;
            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", this.Request.QueryString["actcode"].ToString()+"%", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();
            //this.ddlBuyer.SelectedValue = this.Request.QueryString["actcode"].ToString();

            this.ddlBuyer_SelectedIndexChanged(null, null);
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
                
            string comcod = this.GetComCode();
            Session.Remove("tblmlcorder");
            Session.Remove("tblStockDetails");
            //string mlccode = this.ddlmlccode.SelectedValue.ToString();
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString()+"%";
            string chalndate= Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_ORDERWISE_INVOICE", buyer, chalndate, "", "", "", "", "");
           DataView dv = ds2.Tables[0].DefaultView;

            this.ddlprocode.DataSource = dv.ToTable(true, "invno", "invno1");
            this.ddlprocode.DataTextField = "invno1";
            this.ddlprocode.DataValueField = "invno";
            this.ddlprocode.DataBind();

            if (this.Request.QueryString["genno"].ToString().Length != 0)
            {
                this.ddlprocode.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
            Session["tblmlcorder"] = ds2.Tables[0];
            Session["tblStockDetails"] = ds2.Tables[1];
            LoadPolist();
        }
        private void Loadtrans()
        {
            string comcod = this.GetComCode();
            DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", "%", "", "", "", "", "", "", "", "");

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "sircode like '9901020%' or sircode like '9901030%'";
            this.ddltrans.DataSource = dv.ToTable();
            this.ddltrans.DataTextField = "sirdesc";
            this.ddltrans.DataValueField = "sircode";
            this.ddltrans.DataBind();
        }
        private void LoadPolist()
        {
            DataTable dt = (DataTable)Session["tblmlcorder"];
            string invno = this.ddlprocode.SelectedValue.ToString();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "invno='"+invno+"'";
            this.DdlPoList.DataSource = dv.ToTable(true, "po");
            this.DdlPoList.DataTextField = "po";
            this.DdlPoList.DataValueField = "po";
            this.DdlPoList.DataBind();
            DdlPoList_SelectedIndexChanged(null, null);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);

            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = (this.Request.QueryString["Type"].ToString() == "Approve") ? true : false; ;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you agree to approve?')";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = (this.Request.QueryString["Type"].ToString() == "Edit") ? true : false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (this.Request.QueryString["Type"].ToString() == "Entry") ? true : false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = (this.Request.QueryString["Type"].ToString() == "Entry") ? true : false; ;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Update";


        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLCCode()
        {

            // string comcod = this.GetComeCode();
            // string filter = "%";
            // DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GETORDERMLCCOD", filter, "", "", "", "", "", "", "");
            //List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            //ViewState["tbllcorder"] = lst;
            // this.ddlmlccode.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            // this.ddlmlccode.DataTextField = "mlcdesc";
            // this.ddlmlccode.DataValueField = "mlccod";
            // this.ddlmlccode.DataBind();

            // if (Request.QueryString["actcode"].ToString() != "")
            // {
            //     this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();            
            //     this.ddlmlccode.Enabled = false;
            // }
            // ds2.Dispose();
            // this.ddlmlccode_SelectedIndexChanged(null, null);

            //     if (this.Request.QueryString["Type"].ToString() == "Approve")
            //     {
            //         this.PreviousList();
            //         this.ddlPrevList.SelectedValue = this.Request.QueryString["sircode"].ToString();
            //         this.lbtnOk_Click(null, null);
            //     }
            //     else
            //     {
            //         this.ddlprocode.SelectedValue = this.Request.QueryString["genno"].ToString();
            //     }


        }
        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string comcod = this.GetComeCode();
            //string mlccode = this.ddlmlccode.SelectedValue.ToString();       
            //DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_ORDERWISE_INVOICE", mlccode, "", "", "", "", "", "");
            //DataView dv = ds2.Tables[0].DefaultView;

            //this.ddlprocode.DataSource = dv.ToTable(true, "invno", "invno1");
            //this.ddlprocode.DataTextField = "invno1";
            //this.ddlprocode.DataValueField = "invno";
            //this.ddlprocode.DataBind();

            //ViewState["tblmlcorder"] = ds2.Tables[0];

        }

        private void ShowShiplineType()
        {
            string comcod = GetComCode();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_DESPATCHMODE", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.DDLDespatch.DataTextField = "gdesc";
            this.DDLDespatch.DataValueField = "gcod";
            this.DDLDespatch.DataSource = ds6.Tables[0];
            this.DDLDespatch.DataBind();

        }

        private void ShowOtherInfo()
        {
            string comcod = GetComCode();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETCOUNTRYPORT", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.PrintDeliveryChallan();

        }
        private void PrintDeliveryChallan()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

                string session = hst["session"].ToString();
                string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string printType = this.ddlPrntType.SelectedValue;
                string callType = printType=="2" ? "GET_CHALLAN_PRINT_INFO": "GET_CHALLAN_INFO";
                string invno = this.Request.QueryString["genno"].ToString() == "" ? this.Request.QueryString["sircode"].ToString() : this.Request.QueryString["genno"].ToString();
                DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", callType, invno, "", "", "", "", "", "", "", "");
                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();
                DataTable dt = ds1.Tables[1];
                double tAmt = lst.Select(p => p.totlprs).Sum();

                LocalReport rpt1 = new LocalReport();

                if (printType == "2")
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptDeliveryChallanEdison2", lst, null, null);
                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("invrefno", ds1.Tables[0].Rows[0]["invrefno"].ToString()));
                    rpt1.SetParameters(new ReportParameter("deport", ds1.Tables[1].Rows[0]["deport"].ToString()));
                    rpt1.SetParameters(new ReportParameter("transport", ds1.Tables[1].Rows[0]["transport"].ToString()));

                }
                else
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptDeliveryChallanEdison", lst, null, null);
                    rpt1.EnableExternalImages = true;

                }


                rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("custName", dt.Rows[0]["custdesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("custadd", dt.Rows[0]["custadd"].ToString()));
                rpt1.SetParameters(new ReportParameter("custCuntry", dt.Rows[0]["custcountry"].ToString()));
                rpt1.SetParameters(new ReportParameter("RefNo", dt.Rows[0]["refno"].ToString()));
                rpt1.SetParameters(new ReportParameter("dadd", dt.Rows[0]["exdisdesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("Discharge", dt.Rows[0]["exdisdesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("MofPay", dt.Rows[0]["paymode"].ToString()));
                rpt1.SetParameters(new ReportParameter("contact", dt.Rows[0]["cntctperson"].ToString() + " -" + dt.Rows[0]["contact"].ToString()));
                rpt1.SetParameters(new ReportParameter("totlctn", Convert.ToDouble(lst.Select(p => p.totlctn).Sum()).ToString()));
                rpt1.SetParameters(new ReportParameter("ordrno", ds1.Tables[0].Rows[0]["orderno"].ToString()));
                rpt1.SetParameters(new ReportParameter("artclno", dt.Rows[0]["artno"].ToString()));
                rpt1.SetParameters(new ReportParameter("colordesc", ds1.Tables[0].Rows[0]["colordesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("styledesc", ds1.Tables[0].Rows[0]["styledesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("invno", dt.Rows[0]["dchno1"].ToString()));
                rpt1.SetParameters(new ReportParameter("invdate", Convert.ToDateTime(dt.Rows[0]["deldate"]).ToString("dd-MMM-yyyy")));

                rpt1.SetParameters(new ReportParameter("VechNo", dt.Rows[0]["vehclno"].ToString()));
                rpt1.SetParameters(new ReportParameter("sealno", dt.Rows[0]["sealno"].ToString()));
                rpt1.SetParameters(new ReportParameter("DechBy", dt.Rows[0]["despatchtype"].ToString()));
                rpt1.SetParameters(new ReportParameter("comaddf", dt.Rows[0]["comaddf"].ToString()));

                rpt1.SetParameters(new ReportParameter("txtPreparedBy", dt.Rows[0]["postedname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["posteddat"]));
                rpt1.SetParameters(new ReportParameter("txtReviewBy", dt.Rows[0]["reviwname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["revidat"]));
                rpt1.SetParameters(new ReportParameter("txtAuthBy", dt.Rows[0]["authname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["authdat"]));
                rpt1.SetParameters(new ReportParameter("txtApprovedBy", dt.Rows[0]["appname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["aprvdat"]));

                rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2).Replace("Taka", "Pair")));

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        private void Data_Bind()
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            if (lst.Count == 0)
            {
                this.gvSalCon.DataSource = null;
                this.gvSalCon.DataBind();
                return;
            }
            if (this.GetComCode() != "5301")
            {
                this.gvSalCon.Columns[11].Visible = false;
            }
            this.gvSalCon.DataSource = lst;
            this.gvSalCon.DataBind();
            this.Footcalculation();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            DataTable tbl1 = (DataTable)Session["tblmlcorder"];
            string invno = this.ddlprocode.SelectedValue.ToString();
            string pono = this.DdlPoList.SelectedValue.ToString();
            string product = this.DdlProduct.SelectedValue.ToString();
            string location = this.DdlLocation.SelectedValue.ToString();
            string slrowid = this.slrowid.Text.ToString();
            

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            DataView dv = tbl1.DefaultView;
            dv.RowFilter = "invno='" + invno + "' and po='"+ pono + "' and rescode1='"+ product + "'";
            DataTable dt2 = dv.ToTable();
            foreach (DataRow dtRow in dt2.Rows)
            {
                DataTable dt = (DataTable)Session["tblStockDetails"];
                DataView dv2 = dt.DefaultView;
                dv2.RowFilter = "rescode1='" + product + "' and location='"+ location + "'";

                double locstockqty= Convert.ToDouble(dv2.ToTable().Rows[0]["stockqty"]);
                string mlccode = dtRow["mlccod"].ToString();
                string mlcdesc = dtRow["mlcdesc"].ToString();
                string styleid = dtRow["styleid"].ToString();
                string styledesc = dtRow["styledesc"].ToString();
                string colorid = dtRow["colorid"].ToString();
                string colordesc = dtRow["colordesc"].ToString();
                string sizeid = dtRow["sizeid"].ToString();
                string sizedesc = dtRow["sizedesc"].ToString();
                string rdayid = dtRow["rdayid"].ToString();
                double invqty = Convert.ToDouble(dtRow["invqty"]);
                double totlprs = (invqty >= locstockqty) ? locstockqty : invqty;
                double totlctn = Convert.ToDouble(dtRow["totlctn"]);
               
                string locdesc = dv2.ToTable().Rows[0]["locationdesc"].ToString();
                string wearhouse = dtRow["wearhouse"].ToString(); 
                string po = dtRow["po"].ToString();
                double chlntotal = Convert.ToDouble(dtRow["chlntotal"].ToString());
                double stockqty = locstockqty;
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.invno == invno && p.rdayid == rdayid && p.location==location && p.po==po);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                                        
                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(invno,  mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, totlprs, totlctn, invqty, rdayid, location, locdesc, wearhouse,po, stockqty, invqty, chlntotal, Convert.ToInt32(slrowid)));
            }
            int slrowid1 = Convert.ToInt32(slrowid);
            slrowid1 = slrowid1+1;
            this.slrowid.Text = slrowid1.ToString();

            ViewState["tblexport"] = lst.OrderByDescending(x => x.slrowid).ToList() ;
            this.Data_Bind();
        }

        protected void AddAll_Click(object sender, EventArgs e)
        {
            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            DataTable tbl1 = (DataTable)Session["tblmlcorder"];
            string invno = this.ddlprocode.SelectedValue.ToString();
            string pono = this.DdlPoList.SelectedValue.ToString();
            string slrowid = this.slrowid.ToString();


            DataView dv = tbl1.DefaultView;
            dv.RowFilter = "invno='" + invno + "' and po='" + pono + "'";
            DataTable dt2 = dv.ToTable();
            foreach (DataRow dtRow in dt2.Rows)
            {
              
                string mlccode = dtRow["mlccod"].ToString();
                string mlcdesc = dtRow["mlcdesc"].ToString();
                string styleid = dtRow["styleid"].ToString();
                string styledesc = dtRow["styledesc"].ToString();
                string colorid = dtRow["colorid"].ToString();
                string colordesc = dtRow["colordesc"].ToString();
                string sizeid = dtRow["sizeid"].ToString();
                string sizedesc = dtRow["sizedesc"].ToString();
                string rdayid = dtRow["rdayid"].ToString();

                //DataTable dt = (DataTable)Session["tblStockDetails"];

                //DataView dv2 = dt.DefaultView;
                //dv2.RowFilter = "rescode1='" + mlccode+rdayid+styleid+colorid+sizeid + "' and location='" + location + "'";

                //double locstockqty = Convert.ToDouble(dv2.ToTable().Rows[0]["stockqty"]);


                double totlprs = Convert.ToDouble(dtRow["totlprs"]);
                double totlctn = Convert.ToDouble(dtRow["totlctn"]);
                double invqty = Convert.ToDouble(dtRow["invqty"]);
                string location = dtRow["location"].ToString();
                string locdesc = dtRow["locdesc"].ToString();
                string wearhouse = dtRow["wearhouse"].ToString();
                string po = dtRow["po"].ToString();
                double chlntotal = Convert.ToDouble(dtRow["chlntotal"].ToString());

                double stockqty = Convert.ToDouble(dtRow["stockqty"]);
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.invno == invno && p.rdayid == rdayid && p.location == location);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);

                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(invno, mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, totlprs, totlctn, invqty, rdayid, location, locdesc, wearhouse,po, stockqty, invqty, chlntotal, Convert.ToInt32(slrowid)));

            }
            int slrowid1 = Convert.ToInt32(slrowid);
            this.slrowid.Text = slrowid1++.ToString();

            ViewState["tblexport"] = lst.OrderByDescending(x => x.slrowid).ToList() ;
            this.Data_Bind();
        }

        protected void gvSalCon_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString() == "Edit")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You Cannot Delete on Edit Mood');", true);
                return;
            }
            else
            {
                List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
                int rowindex = (this.gvSalCon.PageSize) * (this.gvSalCon.PageIndex) + e.RowIndex;
                lst.RemoveAt(rowindex);
                ViewState["tblexport"] = lst.OrderByDescending(x => x.slrowid).ToList();
                this.Data_Bind();
            }
            
        }
        private void SaveValue()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            lst=lst.OrderByDescending(x => x.slrowid).ToList();
            for (int i = 0; i < this.gvSalCon.Rows.Count; i++)
            {

                double totlprs = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvtotlprs")).Text.Trim());
                double totlctn = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvtotlctn")).Text.Trim());
                string wearhouse = Convert.ToString(((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvwearhouseno")).Text.Trim());
               
                lst[i].totlprs = totlprs;
                lst[i].totlctn = totlctn;
                lst[i].wearhouse = wearhouse;
               
            }
            ViewState["tblexport"] = lst.OrderByDescending(x => x.slrowid).ToList();

            this.Data_Bind();

        }
        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            

            SaveValue();           
            this.Data_Bind();
           
        }

        private bool stockCheker()
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
              DataTable tblstock=  (DataTable)Session["tblStockDetails"];
            Session.Remove("stockmg");
            if (lst.Count > 0)
            {
                var lst1 = lst.Select(x => new { x.mlccod, x.styleid, x.colorid, x.sizeid, x.rdayid, x.location }).Distinct().ToList();

                foreach (var item in lst1)
                {
                    var temptable=lst.Where(x => x.mlccod == item.mlccod).Where(x => x.rdayid == item.rdayid)
                       .Where(x => x.styleid == item.styleid).Where(x => x.colorid == item.colorid).
                       Where(x => x.sizeid == item.sizeid).Where(x => x.location == item.location).ToList();
                    double chlnqty = temptable.Sum(x => x.totlprs);


                    DataRow[] result = tblstock.Select("mlccod='"+item.mlccod+ "' and styleid='" + item.styleid + "' and colorid='" + item.colorid + "' and sizeid='" + item.sizeid + "' and location='" + item.location + "' and odayid='" + item.rdayid + "'");
                    double stockqty = 0;
                    if (result.Count() > 0)
                    {
                         stockqty = Convert.ToDouble("0" + result[0]["stockqty"]);

                    }

                    if (chlnqty > stockqty)
                    {

                        Session["stockmg"] = "Stock Out of "+ temptable[0].mlcdesc+"-"+temptable[0].styledesc+"- Color:"
                            +temptable[0].colordesc+"-size: "+temptable[0].sizedesc;
                        return false;

                    }
                    //else
                    //{
                    //    return true;
                    //}
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void RefBtn_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblexport");
            this.gvSalCon.DataSource = null;
            this.gvSalCon.DataBind();


        }
        private List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> HiddenSameData(List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst)
        {


            string invno = "";
            string mlccod = "";
            string styleid = "";
            string colorid = "";
            string sizeid = "";
            string po = "";
            var list22 = lst.OrderBy(m => m.invno).ThenBy(m => m.mlccod).ThenBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            foreach (SPEENTITY.C_19_Exp.EClassExpBO.EclassExport c1 in list22)
            {
                if (mlccod == c1.mlccod.ToString())
                {
                    c1.mlcdesc = "";
                }
                if (mlccod == c1.mlccod.ToString() && styleid == c1.styleid.ToString())
                {
                    c1.styledesc = "";
                }
                if (mlccod == c1.mlccod.ToString() && styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
                {
                    c1.colordesc = "";
                }
                if (mlccod == c1.mlccod.ToString() && styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && sizeid == c1.sizeid.ToString())
                {
                    c1.sizedesc = "";
                    
                }
                if (mlccod == c1.mlccod.ToString() && styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && sizeid == c1.sizeid.ToString() && po == c1.po.ToString())
                {
                    
                    c1.invqty1 = 0.00;
                }
                mlccod = c1.mlccod.ToString();
                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();
                sizeid = c1.sizeid.ToString();
                po = c1.po.ToString();
            }
            ViewState["tblexport"] = list22.OrderByDescending(x => x.slrowid).ToList();
            return list22;

        }
        protected void Get_INVNO()
        {

            string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mCPRNo = "NEWINV";
            if (this.ddlPrevList.Items.Count > 0)
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();
            if (mCPRNo == "NEWINV")
            {
                string comcod = this.GetComCode();
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_LAST_DCHNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                this.ddlPrevList.DataTextField = "maxno1";
                this.ddlPrevList.DataValueField = "maxno";
                this.ddlPrevList.DataSource = ds2.Tables[0];
                this.ddlPrevList.DataBind();
            }
        }
        private void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            //string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string invno = this.ddlprocode.SelectedValue.ToString();
            string despatchmod = this.DDLDespatch.SelectedValue.ToString();
            string contact = this.txtMobile.Text.Trim().ToString();
            string covervan = this.txtVan.Text.Trim().ToString();
            string sealno = this.TxtSealNo.Text.Trim().ToString();
            //this.SaveValue();
            //if (this.stockCheker() == false)
            //{
            //    string msg = Session["stockmg"].ToString().Replace("'", "").ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //    return;
            //}
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            //if(lst.FindAll(p=>p.stockqty==0 || p.stockqty<0).ToList().Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Unavailable');", true);

            //    return;
            //}

            if (this.GetComCode() == "5301")
            {
                if (lst.FindAll(p => p.location == "00000").ToList().Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Your FG Location Not allocated');", true);

                    return;
                }
            }

            if (this.ddlPrevList.Items.Count == 0)
                this.Get_INVNO();
            string dchno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string deldate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string contactperson = this.txtContactPerson.Text.Trim().ToString();
            string refno = this.txtRefNo.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string type = this.Request.QueryString["Type"].ToString();
            string containerNo = this.txtCntnrNo.Text.Trim();
            string license = this.txtLicnseNo.Text.Trim();
            string trans = this.ddltrans.SelectedValue.Trim();
            string portofdis = this.txtpod.Text.Trim();
            DataTable dt1 = ASITUtility03.ListToDataTable(lst);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_CHALLAN", "UPDATE_CHALLAN_INFO", ds1, null, null, dchno, "000000000000", deldate, refno, mNAR, PostedDat, usrid, sessionid, trmid, contactperson, contact, covervan, sealno, despatchmod, containerNo, license, trans, portofdis);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);
                return;
            }
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            //string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string invno = this.ddlprocode.SelectedValue.ToString();
            string despatchmod = this.DDLDespatch.SelectedValue.ToString();
            string contact = this.txtMobile.Text.Trim().ToString();
            string covervan = this.txtVan.Text.Trim().ToString();
            string sealno = this.TxtSealNo.Text.Trim().ToString();
            this.SaveValue();
            if (this.stockCheker() == false)
            {
                string msg = Session["stockmg"].ToString().Replace("'", "").ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            //if(lst.FindAll(p=>p.stockqty==0 || p.stockqty<0).ToList().Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Stock Unavailable');", true);

            //    return;
            //}

            if (this.GetComCode() == "5301")
            {
                if (lst.FindAll(p => p.location == "00000").ToList().Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Your FG Location Not allocated');", true);

                    return;
                }
            }

            if (this.ddlPrevList.Items.Count == 0)
                this.Get_INVNO();
            string dchno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string deldate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string contactperson = this.txtContactPerson.Text.Trim().ToString();
            string refno = this.txtRefNo.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string type = this.Request.QueryString["Type"].ToString();
            string containerNo = this.txtCntnrNo.Text.Trim();
            string license = this.txtLicnseNo.Text.Trim();
            string trans = this.ddltrans.SelectedValue.Trim();
            string portofdis = this.txtpod.Text.Trim();

            DataTable dt1 = ASITUtility03.ListToDataTable(lst);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_CHALLAN", "UPDATE_CHALLAN_INFO", ds1, null, null, dchno, "000000000000", deldate, refno, mNAR, PostedDat, usrid, sessionid, trmid, contactperson, contact, covervan, sealno, despatchmod, containerNo, license, trans, portofdis);
            
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong');", true);
                return;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                ViewState.Remove("tblexport");
                this.imgPreVious.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();
                this.lblCurNo1.Text = "";
                this.txtCurNo2.Text = "";
                this.txtdate.Enabled = true;
                this.txtRefNo.Text = "";
                //this.ddlmlccode.Enabled = true;
                this.txtBillNarr.Text = "";
                this.gvSalCon.DataSource = null;
                this.gvSalCon.DataBind();
                this.Panel2.Visible = false;
                this.ddlBuyer.Enabled = true;
                this.plnexport.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }
            ddlBuyer_SelectedIndexChanged(null,null);
            this.imgPreVious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.lblCurNo1.Enabled = true;
            this.txtCurNo2.Enabled = true;
            this.Panel2.Visible = true;
            //this.ddlmlccode.Enabled = false;
            this.ddlBuyer.Enabled = false;
            this.plnexport.Visible = true;
            this.lbtnOk.Text = "New";
            this.txtdate.Enabled = false;
            this.Get_Info();

        }

        protected void gvSalCon_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (true)
                //{
                //    int index = e.Row.RowIndex;
                //    ((TextBox)this.gvSalCon.Rows[index].FindControl("txtgvtotlprs")).Text = "fasdf";
                //}


                double stock = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stockqty"));
                double invqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "invqty"));
                double chlntotl = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "chlntotal"));

                if (chlntotl < invqty || stock == 0 || stock < 0)
                {
                    e.Row.ToolTip = "Stock Out";
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
                }




            }
        }

        protected void LbtnAddMoreInv_Click(object sender, EventArgs e)
        {
            this.ddlBuyer_SelectedIndexChanged(null,null);
            
        }

        protected void lbgvsize_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblStockDetails"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvSalCon.Rows[index].FindControl("LblMlccod")).Text.ToString();
            string styleid = ((Label)this.gvSalCon.Rows[index].FindControl("LblStyleid")).Text.ToString();
            string dayid = ((Label)this.gvSalCon.Rows[index].FindControl("LblDayid")).Text.ToString();
            string colorid = ((Label)this.gvSalCon.Rows[index].FindControl("LblgvColorid")).Text.ToString();
            string sizeid = ((Label)this.gvSalCon.Rows[index].FindControl("LblGvSizeid")).Text.ToString();
            

            DataView dv = dt.DefaultView;
            dv.RowFilter = "mlccod ='"+ mlccod + "' and odayid='" + dayid + "' and styleid='" + styleid + "' and colorid='"+ colorid + "' and sizeid='"+ sizeid + "'";
            this.gvStockDetails.DataSource = dv.ToTable();
            this.gvStockDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPolist();
           
        }

        protected void DdlPoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblmlcorder"];
            string invno = this.ddlprocode.SelectedValue.ToString();
            string po = this.DdlPoList.SelectedValue.ToString();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "invno='" + invno + "' and po='"+po+"'";           
            this.DdlProduct.DataSource = dv.ToTable(true, "rescode1", "resdesc1");
            this.DdlProduct.DataTextField = "resdesc1";
            this.DdlProduct.DataValueField = "rescode1";
            this.DdlProduct.DataBind();
            DdlProduct_SelectedIndexChanged(null,null);
        }

        protected void DdlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblStockDetails"];         
           string product = this.DdlProduct.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode1='" + product + "'";
            DataTable temptable = dv.ToTable(true, "location", "locationdesc1");
            DataRow toInsert = temptable.NewRow();
            toInsert["location"] = "00000";
            toInsert["locationdesc1"] = "None";
            // insert in the desired place
            temptable.Rows.Add(toInsert);
            this.DdlLocation.DataSource = temptable;
            this.DdlLocation.DataTextField = "locationdesc1";
            this.DdlLocation.DataValueField = "location";
            this.DdlLocation.DataBind();
        }

        protected void Get_Info()
        {
            string comcod = this.GetComCode();
            string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mCPRNo = "NEWINV";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtdate.Enabled = false;
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();

            }

            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_CHALLAN_INFO", mCPRNo, "", "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();

            if (lst == null)
                return;
            ViewState["tblexport"] = lst.OrderByDescending(x => x.slrowid).ToList();


            if (mCPRNo == "NEWINV")
            {
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_LAST_DCHNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                this.DDLDespatch.SelectedValue = "00000";
                return;
            }

            //this.ddlmlccode.SelectedValue = ds1.Tables[1].Rows[0]["mlccod"].ToString();
            //this.ddlmlccode_SelectedIndexChanged(null, null);      
            //    this.ddlprocode.SelectedValue= ds1.Tables[1].Rows[0]["invno"].ToString();
            this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            this.txtCntnrNo.Text = ds1.Tables[1].Rows[0]["containerno"].ToString();
            this.txtVan.Text = Convert.ToString(ds1.Tables[1].Rows[0]["vehclno"]);
            this.txtContactPerson.Text = Convert.ToString(ds1.Tables[1].Rows[0]["cntctperson"]);
            this.txtLicnseNo.Text = Convert.ToString(ds1.Tables[1].Rows[0]["licenseno"]);
            this.txtMobile.Text = Convert.ToString(ds1.Tables[1].Rows[0]["contact"]);
            this.TxtSealNo.Text = Convert.ToString(ds1.Tables[1].Rows[0]["sealno"]);
            
            this.txtRefNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["dchno1"].ToString().Substring(0, 6); ; //lstord[0].promno1.ToString().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["dchno1"].ToString().Substring(6, 5); //lstord[0].promno1.ToString().Substring(6, 5);
            this.txtdate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["deldate"]).ToString("dd-MMM-yyyy");
            this.DDLDespatch.SelectedValue = ds1.Tables[1].Rows[0]["despatchmod"].ToString();
            this.Data_Bind();
        }
        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }
        private void PreviousList()
        {
            string txtdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComCode();
            string Invno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_PREV_CHALLAN", txtdate, Invno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.DataTextField = "dchno1";
            this.ddlPrevList.DataValueField = "dchno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }
        private void Footcalculation()
        {
            try
            {

                var list1 = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
                if (list1.Count == 0)
                    return;
                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotlprs")).Text = (list1.Select(p => p.totlprs).Sum() == 0.00) ? "0" : list1.Select(p => p.totlprs).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotlctn")).Text = (list1.Select(p => p.totlctn).Sum() == 0.00) ? "0" : list1.Select(p => p.totlctn).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalCon.FooterRow.FindControl("txtFInvqty")).Text = (list1.Select(p => p.invqty1).Sum() == 0.00) ? "0" : list1.Select(p => p.invqty1).Sum().ToString("#,##0;(#,##0); ");


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message+"');", true);
            }
        }
        private void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string dchno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            //string mlccod = this.ddlmlccode.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");


            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_CHALLAN", "APPROVE_CHALLAN_INFO", dchno, "000000000000", PostedDat, usrid, sessionid, trmid, CurDate1);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                return;
            }
        }

    }
}