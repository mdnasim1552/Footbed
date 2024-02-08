using Microsoft.Reporting.WinForms;
using SPEENTITY;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_19_EXP
{
    public partial class CreatePackList : System.Web.UI.Page
    {

        public string prevsize { get; set; }
        public int noRowSpan { get; set; }
        public double Stockqty { get; set; }
        ProcessAccess proc1 = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();

        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtdate.Text = this.Request.QueryString["date"].Length != 0 ? this.Request.QueryString["date"] : System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetSeason();
                this.GetBuyer();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Packing List";

                //  this.txtExpDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                GetGenCode();
                
                if (this.Request.QueryString["actcode"].Length != 0)
                {
                    this.PreviousList();

                    this.lbtnOk_Click(null, null);
                }
            }
        }
        private void GetSeason()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();
            if (Request.QueryString.AllKeys.Contains("sircod") && Request.QueryString["sircod"].Length != 0)
                this.ddlBuyer.SelectedValue = Request.QueryString["sircod"].ToString();
            this.ddlBuyer_SelectedIndexChanged(null, null);
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Text = "Delete Selected";
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkbtnTranList_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnTranList_Click(object sender, EventArgs e)
        {
            //List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            //List<int> lstRemove = new List<int>();

            //for (int i = 0; i < gvSalCon.Rows.Count; i++)
            //{
            //    if (((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked)
            //    {
            //        lstRemove.Add(i);
            //    }
            //}
            
            //lst.RemoveAll(x => lstRemove.Contains(lst.IndexOf(x)));
            //ViewState["tblexport"] = lst;
            //this.Bind_Pack_Allocation();
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).OnClientClick = "return confirm('Do You Want To Delete Selected Item?')";

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = true;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLCCode()
        {
            string comcod = this.GetComeCode();
            string filter = "1601%";
            string Buyer = this.ddlBuyer.SelectedValue.ToString()+"%";
            string season = this.DdlSeason.SelectedValue.ToString()+"%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", filter, Buyer, season, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable tbllc = ds1.Tables[0];
            tbllc.Rows.Add(comcod, "", "", "", "", "", "", "", "", "", "000000000000", "000000000000", "--All--");
            this.ddlmlccode.DataTextField = "styledesc2";
            this.ddlmlccode.DataValueField = "stylecode2";
            this.ddlmlccode.DataSource = tbllc;
            this.ddlmlccode.DataBind();
            this.ddlmlccode.SelectedValue = "000000000000";

            ViewState["tblordstyle"] = ds1.Tables[0];

            ddlmlccode_SelectedIndexChanged(null, null);
        }

        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlmlccode.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            if(mlccode1!="000000000000" && mlccode1 != "")
            {
                dv1.RowFilter = ("stylecode2='" + mlccode1 + "'");

            }
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode2");
            dt1.Rows.Add( "None", "000000000000");

            this.dllorderType.DataTextField = "styledesc2";
            this.dllorderType.DataValueField = "stylecode2";
            this.dllorderType.DataSource = dt1;
            this.dllorderType.SelectedValue = mlccode1;
            this.dllorderType.DataBind();

            if (mlccode1 == "" || mlccode1 == "000000000000")
            {
                return;
            }
            dllorderType_SelectedIndexChanged(null, null);

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }


        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            Save_Value_Packing_allocation();
            this.Bind_Pack_Allocation();
            this.CalculateTotalInFooter();
        }

        private void CalculateTotalInFooter()
        {
            var lstpack = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];

            double sumOfTtlCrtns = lstpack.Sum(x => x.ttlcrtns);
            double sumOfTtlPairs = lstpack.Sum(x => x.ttlpairs);
            double sumOfCbm = lstpack.Sum(x => x.cbm);
            double sumOfTtlGrsWgt = lstpack.Sum(x => x.ttlgrswgt);
            double sumOfTtlNetWgt = lstpack.Sum(x => x.ttlnetwgt);

            ((Label)gv1pack.FooterRow.FindControl("gvLblTtlCrtns")).Text = sumOfTtlCrtns.ToString();
            ((Label)gv1pack.FooterRow.FindControl("PFLblgvTotalPair")).Text = sumOfTtlPairs.ToString();
            ((Label)gv1pack.FooterRow.FindControl("gvLblTtlCbm")).Text = sumOfCbm.ToString("#,##0.00000;(#,##0.00000);");
            ((Label)gv1pack.FooterRow.FindControl("gvLblTtlGrsWgt")).Text = sumOfTtlGrsWgt.ToString("#,##0.00;(#,##0.00);");
            ((Label)gv1pack.FooterRow.FindControl("gvLblTtlNetWgt")).Text = sumOfTtlNetWgt.ToString("#,##0.00;(#,##0.00);");
        }

        protected void RefBtn_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblPackqty");
            this.gv1pack.DataSource = null;
            this.gv1pack.DataBind();


        }
        private List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> HiddenSameData(List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst)
        {

            //string slnum = dt.Rows[0]["slnum"].ToString();
            string styleid = "";
            string colorid = "";
            string sizeid = "";
            //var list22 = lst.OrderBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            var list22 = lst.OrderBy(m => m.mlccod).ThenBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            foreach (SPEENTITY.C_19_Exp.EClassExpBO.EclassExport c1 in list22)
            {
                if (styleid == c1.styleid.ToString())
                {
                    c1.styledesc = "";
                }
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
                {
                    c1.colordesc = "";
                }
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && sizeid == c1.sizeid.ToString())
                {
                    c1.sizedesc = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();
                sizeid = c1.sizeid.ToString();

            }
            ViewState["tblexport"] = list22;
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
                string comcod = this.GetComeCode();
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_LAST_EXPORT_PACK_PLAN", CurDate1, "", "", "", "", "", "", "", "");
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

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            if (this.txtCstmRefNo.Text.Length==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Custom Ref No is Mandatory');", true);
                return;
            }
            else
            {
                string textvalue = this.txtCstmRefNo.Text;
                bool ischars = false;
                string specialChar = @"|!#$%&/()+?»«@";
                foreach (var item in specialChar)
                {
                    if (textvalue.Contains(item))

                    {
                        ischars = true;

                    }
                        
                }
                if (ischars)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Special characters Violation(|!#$%&/()+?»«@)');", true);
                    return;
                }
               
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            //string rdayid = this.dllorderType.SelectedValue.ToString();

            this.Save_Value_Packing_allocation();
            this.CalculateTotalInFooter();

            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];

            if (this.ddlPrevList.Items.Count == 0)
            {
                this.Get_INVNO();
            }
            string eppno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string invdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");


            if (lst2 == null)
                return;

            //string dayid = lst2[0].dayid.ToString() == "" ? lst2[0].dayid.ToString() : this.dllorderType.SelectedValue.ToString().Substring(36, 8);
            string customRefNo = this.txtCstmRefNo.Text.Trim();
            //string mLCCode = this.ddlmlccode.SelectedValue.ToString();

            bool result = false;
            string buyerid = this.ddlBuyer.SelectedValue.ToString();
            string BookingDate =(this.TxtBookingDate.Text.Length==0)?"01-Jan-1900": Convert.ToDateTime(this.TxtBookingDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ExFactoryDate = (this.TxtExFactoryDate.Text.Length == 0) ? "01-Jan-1900" : Convert.ToDateTime(this.TxtExFactoryDate.Text.Trim()).ToString("dd-MMM-yyyy");

            result = proc1.UpdateTransInfo1(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_EXPORT_PACK_DETAILS", "exportpackb", customRefNo, eppno, invdate, usrid, sessionid, trmid, buyerid, BookingDate, ExFactoryDate);

            DataTable dtsizes = (DataTable)ViewState["tblsizedesc"];
            String[] SizeID = new String[dtsizes.Rows.Count];
            int sizeindex = 0;

            foreach (DataRow item in dtsizes.Rows)
            {
                SizeID[sizeindex] = item["sizeid"].ToString();
                sizeindex++;
            }

            for (int i = 0; i < lst2.Count; i++)
            {
                string slnum = lst2[i].slnum;
                string dayid= lst2[i].dayid;
                string pStyleID = lst2[i].styleid;
                string pColorID = lst2[i].colorid;
                string packid = lst2[i].packid;
                string custordno = lst2[i].custordno;
                string CustOrder1 = ((Label)gv1pack.Rows[i].FindControl("lblCustOrder")).Text.Trim().ToString();
                string mlccode = lst2[i].mlccod;
                string custrefno = lst2[i].custrefno;
                string cartoon = lst2[i].cartoon.ToString();
                string crtnno = lst2[i].crtnno;
                string barcodref = lst2[i].barcodrefno;
                string typeoflebel = lst2[i].typeoflebel;

                string boxLength = lst2[i].boxlength.ToString();
                string boxWidth =  lst2[i].boxwidth.ToString();
                string boxHeight = lst2[i].boxheight.ToString();
                string netWgtPerCrtn = lst2[i].netwgtpercrtn.ToString();
                string grsWgtPerCrtn = lst2[i].grswgtpercrtn.ToString();
                string ttlCrtns= lst2[i].ttlcrtns.ToString();



                String[] OrderQty = {
                    lst2[i].s1.ToString(),
                    lst2[i].s2.ToString(),  lst2[i].s3.ToString(),
                    lst2[i].s4.ToString(),  lst2[i].s5.ToString(),
                    lst2[i].s6.ToString(),  lst2[i].s7.ToString(),
                    lst2[i].s8.ToString(),  lst2[i].s9.ToString(),
                    lst2[i].s10.ToString(), lst2[i].s11.ToString(),
                    lst2[i].s12.ToString(), lst2[i].s13.ToString(),
                    lst2[i].s14.ToString(), lst2[i].s15.ToString(),
                    lst2[i].s16.ToString(), lst2[i].s17.ToString(),
                    lst2[i].s18.ToString(), lst2[i].s19.ToString(),
                    lst2[i].s20.ToString(), lst2[i].s21.ToString(),
                    lst2[i].s22.ToString(), lst2[i].s23.ToString(),
                    lst2[i].s24.ToString(), lst2[i].s25.ToString(),
                    lst2[i].s26.ToString(), lst2[i].s27.ToString(),
                    lst2[i].s28.ToString(), lst2[i].s29.ToString(),
                    lst2[i].s30.ToString(), lst2[i].s31.ToString(),
                    lst2[i].s32.ToString(), lst2[i].s33.ToString(),
                    lst2[i].s34.ToString(), lst2[i].s35.ToString(),
                    lst2[i].s36.ToString(), lst2[i].s37.ToString(),
                    lst2[i].s38.ToString(), lst2[i].s39.ToString(),
                    lst2[i].s40.ToString()
                };

                for (int j = 0; j < SizeID.Length; j++)
                {
                   // string totalpr = (Convert.ToDouble(OrderQty[j]) * Convert.ToDouble(ttlCrtns)).ToString();
                    result = proc1.UpdateTransInfo1(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_EXPORT_PACK_DETAILS", mlccode, dayid, pStyleID, pColorID, packid, cartoon,
                                   SizeID[j], OrderQty[j], custordno, custrefno, slnum, eppno, invdate, usrid, sessionid, trmid, barcodref, crtnno, boxLength, boxWidth, boxHeight, netWgtPerCrtn, grsWgtPerCrtn, ttlCrtns, typeoflebel, CustOrder1);
                }

            }

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
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
                this.gv1pack.DataSource = null;
                this.gv1pack.DataBind();
                this.lbtnOk.Text = "Ok";
                this.SelectionPanel.Visible = false;
                this.Btnpanel.Visible = false;
                this.ddlBuyer.Enabled = true;//System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ddlmlccode.Enabled = true;
                this.dllorderType.Enabled = true;
                return;
            }
            if (this.ddlmlccode.SelectedValue.ToString().Length == 0 && this.ddlPrevList.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Article Not Found');", true);

                return;
            }
            this.imgPreVious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.lblCurNo1.Enabled = true;
            this.txtCurNo2.Enabled = true;
            this.ddlmlccode.Enabled = false;
            this.dllorderType.Enabled = false;
            this.lbtnOk.Text = "New";
            this.ddlBuyer.Enabled = false;
            this.SelectionPanel.Visible = true;
            this.Btnpanel.Visible = true;
            this.Get_Info();

        }

        protected void Get_Info()
        {
            try
            {
                string comcod = this.GetComeCode();
                string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string mCPRNo = "NEWINV";
                string mlccod = String.Empty;
                string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
                string styleid = String.Empty;
                string colorid = String.Empty;
                string dayid = String.Empty;
                if (this.ddlPrevList.Items.Count > 0)
                {
                    this.txtdate.Enabled = false;
                    mCPRNo = this.ddlPrevList.SelectedValue.ToString();

                }
                else
                {
                    mlccod = this.ddlmlccode.SelectedValue.ToString().Substring(0, 12);

                    styleid = this.dllorderType.SelectedValue.ToString().Substring(12, 12);
                    colorid = this.dllorderType.SelectedValue.ToString().Substring(24, 12);
                    dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);
                }


                DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_EXPORT_PACKING_DETAILS", mCPRNo, mlccod, dayid, styleid, season, "", "", "", "", "");

                if (this.ddlPrevList.Items.Count > 0)
                {
                    string s = ds1.Tables[0].Rows[0]["mlccod"].ToString();
                    this.ddlBuyer.SelectedValue = ds1.Tables[0].Rows[0]["buyerid"].ToString();
                    this.ddlBuyer_SelectedIndexChanged(null,null);
                    this.ddlmlccode.SelectedValue = s;
                    this.ddlmlccode_SelectedIndexChanged(null,null);
                    string stylecode2 = ds1.Tables[0].Rows[0]["mlccod"].ToString() +
                        ds1.Tables[0].Rows[0]["styleid"].ToString() + ds1.Tables[0].Rows[0]["colorid"].ToString()
                        + ds1.Tables[0].Rows[0]["dayid"].ToString();
                    this.dllorderType.SelectedValue = stylecode2.Trim();
                    //this.dllorderType_SelectedIndexChanged(null,null);
                    var PackNos = this.ddlPrevList.SelectedItem.ToString().Split('-');
                    this.txtCurNo2.Text = PackNos[1].ToString().Substring(0,5);
                    this.lblCurNo1.Text = PackNos[0].ToString() + "-";
                    this.txtCstmRefNo.Text = ds1.Tables[2].Rows[0]["packplanref"].ToString();
                    this.TxtBookingDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["bookdat"]).ToString("dd-MMM-yyyy");
                    this.TxtExFactoryDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["exfactdat"]).ToString("dd-MMM-yyyy");

                }
                if (ds1 == null)
                    return;

                ViewState["tblcollapseTwo"] = ds1.Tables[3];
                this.gvcollapseTwo.DataSource = (DataTable)ViewState["tblcollapseTwo"];
                this.gvcollapseTwo.DataBind();

                ViewState["tblsizedesc"] = ds1.Tables[1];
                string mStyleID = "xxxxxxx";
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                        ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                    mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
                }

                ViewState["tblPackqty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>().OrderBy(p => p.slnum).ToList();

                for (int i = 18; i < 47; i++)
                    this.gv1pack.Columns[i].Visible = false;

                for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                {

                    int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                    this.gv1pack.Columns[columid + 17].Visible = true;
                    this.gv1pack.Columns[columid + 17].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
                }
                this.gv1pack.EditIndex = -1;
                this.Bind_Pack_Allocation();
                this.GetArticleWiseCustomerPO();
                this.HypPi.NavigateUrl = "~/F_03_CostABgd/SalesContact?Type=Entry&actcode= " + this.ddlmlccode.SelectedValue.ToString() +
                                                "&dayid=" + this.dllorderType.SelectedValue.ToString().Trim().Substring(36, 8);
                if (mCPRNo == "NEWINV")
                {
                    DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_LAST_EXPORT_PACK_PLAN", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                    return;
                }

                

                this.CalculateTotalInFooter();

                //this.txtGTotal.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["invno1"]).ToString("#,##0.00;(#,##0.00); ");


                //this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["invno1"].ToString().Substring(0, 6); ; //lstord[0].promno1.ToString().Substring(0, 6);
                //this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["invno1"].ToString().Substring(6, 5); //lstord[0].promno1.ToString().Substring(6, 5);
                //this.txtdate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");

                //this.ddlBuyer.SelectedValue = ds1.Tables[1].Rows[0]["custid"].ToString();

                ////this.dllorderType.SelectedValue = ds1.Tables[1].Rows[0]["rdayid"].ToString();            

                //this.ddlBuyer_SelectedIndexChanged(null, null);
                //// this.ddlmlccode.SelectedValue = ds1.Tables[1].Rows[0]["mlccod"].ToString();
                //this.ddlmlccode_SelectedIndexChanged(null, null);


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Ensure Selected Desire Buyer";
            }


        }
        private void GetArticleWiseCustomerPO()
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString().Substring(0, 12);
            string styleid = this.dllorderType.SelectedValue.ToString().Substring(12, 12);
            string colorid = this.dllorderType.SelectedValue.ToString().Substring(24, 12);
            string dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ARTICLE_WISE_CUSTOMER_ORDER", mlccod, dayid, styleid, colorid, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.DdlCustorder.DataTextField = "custordno";
            this.DdlCustorder.DataValueField = "custordno";
            this.DdlCustorder.DataSource = ds1.Tables[0];
            this.DdlCustorder.DataBind();
            Session["tblPOsum"] = ds1.Tables[0];
            DdlCustorder_SelectedIndexChanged(null, null);
        }

        protected void DdlCustorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblPOsum"];

            string styleid = this.dllorderType.SelectedValue.ToString().Substring(12, 12);
            string colorid = this.dllorderType.SelectedValue.ToString().Substring(24, 12);
            string dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);
            string custorderno = this.DdlCustorder.SelectedValue.ToString();

            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = "custordno='" + custorderno + "' and dayid='" + dayid + "' and styleid='" + styleid + "' and colorid='" + colorid + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                this.txtPoQty.Text = Convert.ToDouble(dv.ToTable().Rows[0]["ordrqty"]).ToString("#,##0");


            }

        }
        private void Bind_Pack_Allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            //List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = list;
            this.gv1pack.DataSource = list;
            this.gv1pack.DataBind();
            // this.FooterCalPackList();
            //this.OrderINput_Selection();
        }
        private void FooterCalPackList()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            if (list == null || list.Count == 0)
            {
                return;
            }
    ((Label)this.gv1pack.FooterRow.FindControl("flblgvF1")).Text = ((list.Sum(p => p.p1) == 0) ? 0 : list.Sum(p => p.p1)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF2")).Text = ((list.Sum(p => p.p2) == 0) ? 0 : list.Sum(p => p.p2)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF3")).Text = ((list.Sum(p => p.p3) == 0) ? 0 : list.Sum(p => p.p3)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF4")).Text = ((list.Sum(p => p.p4) == 0) ? 0 : list.Sum(p => p.p4)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF5")).Text = ((list.Sum(p => p.p5) == 0) ? 0 : list.Sum(p => p.p5)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF6")).Text = ((list.Sum(p => p.p6) == 0) ? 0 : list.Sum(p => p.p6)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF7")).Text = ((list.Sum(p => p.p7) == 0) ? 0 : list.Sum(p => p.p7)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF8")).Text = ((list.Sum(p => p.p8) == 0) ? 0 : list.Sum(p => p.p8)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF9")).Text = ((list.Sum(p => p.p9) == 0) ? 0 : list.Sum(p => p.p9)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF10")).Text = ((list.Sum(p => p.p10) == 0) ? 0 : list.Sum(p => p.p10)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF11")).Text = ((list.Sum(p => p.p11) == 0) ? 0 : list.Sum(p => p.p11)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF12")).Text = ((list.Sum(p => p.p12) == 0) ? 0 : list.Sum(p => p.p12)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF13")).Text = ((list.Sum(p => p.p13) == 0) ? 0 : list.Sum(p => p.p13)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF14")).Text = ((list.Sum(p => p.p14) == 0) ? 0 : list.Sum(p => p.p14)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF15")).Text = ((list.Sum(p => p.p15) == 0) ? 0 : list.Sum(p => p.p15)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF16")).Text = ((list.Sum(p => p.p16) == 0) ? 0 : list.Sum(p => p.p16)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF17")).Text = ((list.Sum(p => p.p17) == 0) ? 0 : list.Sum(p => p.p17)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF18")).Text = ((list.Sum(p => p.p18) == 0) ? 0 : list.Sum(p => p.p18)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF19")).Text = ((list.Sum(p => p.p19) == 0) ? 0 : list.Sum(p => p.p19)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF20")).Text = ((list.Sum(p => p.p20) == 0) ? 0 : list.Sum(p => p.p20)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ") + " CTN";
            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotalPair")).Text = ((list.Sum(p => p.psum) == 0) ? 0 : list.Sum(p => p.psum)).ToString("#,##0;(#,##0); ") + " PRS";


        }
        private List<SPEENTITY.C_01_Mer.GetOrderWithCat> HiddenSameDataPack(List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst)
        {

            string styleid = "";
            string colorid = "";
            string packid = "";
            if (lst == null)
                return new List<SPEENTITY.C_01_Mer.GetOrderWithCat>();

            foreach (SPEENTITY.C_01_Mer.GetOrderWithCat c1 in lst)
            {
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && packid == c1.packid.Substring(0, 3).ToString())
                {
                    c1.description = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();
                packid = c1.packid.Substring(0, 3).ToString();
            }

            return lst;

        }
        private void Save_Value_Packing_allocation()
        {
            try
            {
                //////////// packing /////////////////
                List<SPEENTITY.C_01_Mer.GetOrderWithCat> lstpack = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
                List<SPEENTITY.C_01_Mer.GetOrderWithCat> lstpacknew = new List<SPEENTITY.C_01_Mer.GetOrderWithCat>();
                if (lstpack == null)
                    return;
                for (int i = 0; i < this.gv1pack.Rows.Count; i++)
                {
                    string custordno = ((TextBox)gv1pack.Rows[i].FindControl("TxtCustOrder")).Text.Trim().ToString();
                    string custordno1 = ((Label)gv1pack.Rows[i].FindControl("lblCustOrder")).Text.Trim().ToString();
                    string crtnNo = ((TextBox)gv1pack.Rows[i].FindControl("txtCrtnNo")).Text.Trim().ToString();
                    string barCodeRefNo = ((TextBox)gv1pack.Rows[i].FindControl("txtBarCdRefNo")).Text.Trim().ToString();
                    string custrefno = ((TextBox)gv1pack.Rows[i].FindControl("TxtRefno")).Text.ToString();
                    string hscode = ((Label)gv1pack.Rows[i].FindControl("gv1LblHsCode")).Text.ToString();
                    string typeoflebel = ((TextBox)gv1pack.Rows[i].FindControl("gv1TxtTypeOfLbl")).Text.ToString();

                    double boxLength = Convert.ToDouble(((TextBox)gv1pack.Rows[i].FindControl("lblBoxLength")).Text.Trim() == "" ? "0.00" : ((TextBox)gv1pack.Rows[i].FindControl("lblBoxLength")).Text.Trim());
                    double boxWidth = Convert.ToDouble(((TextBox)gv1pack.Rows[i].FindControl("lblBoxWidth")).Text.Trim() == "" ? "0.00" : ((TextBox)gv1pack.Rows[i].FindControl("lblBoxWidth")).Text.Trim());
                    double boxHeight = Convert.ToDouble(((TextBox)gv1pack.Rows[i].FindControl("lblBoxHeight")).Text.Trim() == "" ? "0.00" : ((TextBox)gv1pack.Rows[i].FindControl("lblBoxHeight")).Text.Trim());
                    
                    string packid = ((DropDownList)gv1pack.Rows[i].FindControl("DdlPacklist")).SelectedValue.ToString();
                    double carton = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("Ptxtcarton")).Text.Trim()));
                    //double pairsPerCrtn = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("txtPrsPerCrtn")).Text.Trim()));
                    double ttlCrtn = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("txtTtlCrtn")).Text.Trim()));
                    double netWgtPerCrtn = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("txtNetWgt")).Text.Trim()));
                    double grsWgtPerCrtn = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("txtGrsWgt")).Text.Trim()));

                    double s1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF1")).Text.Trim()));
                    double s2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF2")).Text.Trim()));
                    double s3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF3")).Text.Trim()));
                    double s4 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF4")).Text.Trim()));
                    double s5 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF5")).Text.Trim()));
                    double s6 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF6")).Text.Trim()));
                    double s7 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF7")).Text.Trim()));
                    double s8 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF8")).Text.Trim()));
                    double s9 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF9")).Text.Trim()));
                    double s10 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF10")).Text.Trim()));
                    double s11 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF11")).Text.Trim()));
                    double s12 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF12")).Text.Trim()));
                    double s13 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF13")).Text.Trim()));
                    double s14 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF14")).Text.Trim()));
                    double s15 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF15")).Text.Trim()));
                    double s16 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF16")).Text.Trim()));
                    double s17 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF17")).Text.Trim()));
                    double s18 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF18")).Text.Trim()));
                    double s19 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF19")).Text.Trim()));
                    double s20 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF20")).Text.Trim()));
                    double s21 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF21")).Text.Trim()));
                    double s22 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF22")).Text.Trim()));
                    double s23 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF23")).Text.Trim()));
                    double s24 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF24")).Text.Trim()));
                    double s25 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF25")).Text.Trim()));
                    double s26 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF26")).Text.Trim()));
                    double s27 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF27")).Text.Trim()));
                    double s28 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF28")).Text.Trim()));
                    double s29 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF29")).Text.Trim()));
                    double s30 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF30")).Text.Trim()));
                    double s31 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF31")).Text.Trim()));
                    double s32 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF32")).Text.Trim()));
                    double s33 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF33")).Text.Trim()));
                    double s34 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF34")).Text.Trim()));
                    double s35 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF35")).Text.Trim()));
                    double s36 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF36")).Text.Trim()));
                    double s37 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF37")).Text.Trim()));
                    double s38 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF38")).Text.Trim()));
                    double s39 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF39")).Text.Trim()));
                    double s40 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1pack.Rows[i].FindControl("PtxtgvF40")).Text.Trim()));

                    lstpack[i].custordno = (custordno1 == custordno ? custordno1.Replace(" ", String.Empty) : custordno.Replace(" ", String.Empty));
                    lstpack[i].custrefno = custrefno;
                    lstpack[i].hscode = hscode;
                    lstpack[i].packid = packid;
                    lstpack[i].cartoon = carton;
                    lstpack[i].s1 = s1;
                    lstpack[i].s2 = s2;
                    lstpack[i].s3 = s3;
                    lstpack[i].s4 = s4;
                    lstpack[i].s5 = s5;
                    lstpack[i].s6 = s6;
                    lstpack[i].s7 = s7;
                    lstpack[i].s8 = s8;
                    lstpack[i].s9 = s9;
                    lstpack[i].s10 = s10;
                    lstpack[i].s11 = s11;
                    lstpack[i].s12 = s12;
                    lstpack[i].s13 = s13;
                    lstpack[i].s14 = s14;
                    lstpack[i].s15 = s15;
                    lstpack[i].s16 = s16;
                    lstpack[i].s17 = s17;
                    lstpack[i].s18 = s18;
                    lstpack[i].s19 = s19;
                    lstpack[i].s20 = s20;
                    lstpack[i].s21 = s21;
                    lstpack[i].s22 = s22;
                    lstpack[i].s23 = s23;
                    lstpack[i].s24 = s24;
                    lstpack[i].s25 = s25;
                    lstpack[i].s26 = s26;
                    lstpack[i].s27 = s27;
                    lstpack[i].s28 = s28;
                    lstpack[i].s29 = s29;
                    lstpack[i].s30 = s30;
                    lstpack[i].s31 = s31;
                    lstpack[i].s32 = s32;
                    lstpack[i].s33 = s33;
                    lstpack[i].s34 = s34;
                    lstpack[i].s35 = s35;
                    lstpack[i].s36 = s36;
                    lstpack[i].s37 = s37;
                    lstpack[i].s38 = s38;
                    lstpack[i].s39 = s39;
                    lstpack[i].s40 = s10;
                    lstpack[i].totalqty = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                        s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);

                    lstpack[i].typeoflebel = typeoflebel;
                    lstpack[i].crtnno = crtnNo;
                    lstpack[i].barcodrefno = barCodeRefNo;
                    lstpack[i].boxlength = boxLength;
                    lstpack[i].boxwidth = boxWidth;
                    lstpack[i].boxheight = boxHeight;
                    lstpack[i].cbm = (boxLength * boxWidth * boxHeight * ttlCrtn) / 1000000;
                    lstpack[i].pairspercrtn = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                        s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);

                    lstpack[i].ttlcrtns = ttlCrtn;
                    lstpack[i].ttlpairs = lstpack[i].ttlcrtns * lstpack[i].pairspercrtn;

                    lstpack[i].netwgtpercrtn = netWgtPerCrtn;
                    lstpack[i].ttlnetwgt = lstpack[i].ttlcrtns * lstpack[i].netwgtpercrtn;

                    lstpack[i].grswgtpercrtn = grsWgtPerCrtn;
                    lstpack[i].ttlgrswgt = lstpack[i].ttlcrtns * lstpack[i].grswgtpercrtn;


                    lstpack[i].p1 = s1 * carton;
                    lstpack[i].p2 = s2 * carton;
                    lstpack[i].p3 = s3 * carton;
                    lstpack[i].p4 = s4 * carton;
                    lstpack[i].p5 = s5 * carton;
                    lstpack[i].p6 = s6 * carton;
                    lstpack[i].p7 = s7 * carton;
                    lstpack[i].p8 = s8 * carton;
                    lstpack[i].p9 = s9 * carton;
                    lstpack[i].p10 = s10 * carton;
                    lstpack[i].p11 = s11 * carton;
                    lstpack[i].p12 = s12 * carton;
                    lstpack[i].p13 = s13 * carton;
                    lstpack[i].p14 = s14 * carton;
                    lstpack[i].p15 = s15 * carton;
                    lstpack[i].p16 = s16 * carton;
                    lstpack[i].p17 = s17 * carton;
                    lstpack[i].p18 = s18 * carton;
                    lstpack[i].p19 = s19 * carton;
                    lstpack[i].p20 = s20 * carton;
                    lstpack[i].p21 = s21 * carton;
                    lstpack[i].p22 = s22 * carton;
                    lstpack[i].p23 = s23 * carton;
                    lstpack[i].p24 = s24 * carton;
                    lstpack[i].p25 = s25 * carton;
                    lstpack[i].p26 = s26 * carton;
                    lstpack[i].p27 = s27 * carton;
                    lstpack[i].p28 = s28 * carton;
                    lstpack[i].p29 = s29 * carton;
                    lstpack[i].p30 = s30 * carton;
                    lstpack[i].p31 = s31 * carton;
                    lstpack[i].p32 = s32 * carton;
                    lstpack[i].p33 = s33 * carton;
                    lstpack[i].p34 = s34 * carton;
                    lstpack[i].p35 = s35 * carton;
                    lstpack[i].p36 = s36 * carton;
                    lstpack[i].p37 = s37 * carton;
                    lstpack[i].p38 = s38 * carton;
                    lstpack[i].p39 = s39 * carton;
                    lstpack[i].p40 = s10 * carton;

                    lstpack[i].psum = lstpack[i].p1 + lstpack[i].p2 + lstpack[i].p3 + lstpack[i].p4 + lstpack[i].p5 + lstpack[i].p6 + lstpack[i].p7 + lstpack[i].p8 + lstpack[i].p9 + lstpack[i].p10 + lstpack[i].p11 + lstpack[i].p12;
                    lstpacknew.Insert(lstpacknew.Count, lstpack[i]);
                }
                ViewState["tblPackqty"] = lstpack;
            }
            catch (Exception ex)
            {

            }
            
        }


        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.Save_Value_Packing_allocation();
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string styleid = lst[RowIndex].styleid.ToString();
            string colorid = lst[RowIndex].colorid.ToString();
            string packid = lst[RowIndex].packid.ToString();
            string custord = lst[RowIndex].custordno.ToString();
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> coplist = lst.FindAll(p => p.styleid == styleid && p.colorid == colorid && p.packid == packid && p.custordno == custord);
            SPEENTITY.C_01_Mer.GetOrderWithCat coplist1 = lst.FirstOrDefault(p => p.styleid == styleid && p.colorid == colorid && p.packid == packid && p.custordno == custord);
            string slnum = "000";
            //List<SPEENTITY.C_01_Mer.GetOrderWithCat> finallst =
            if (lst.Count > 0)
            {
                slnum = Convert.ToInt32(lst.Max(x=>x.slnum)).ToString();

            }
            string slnum1 = ASTUtility.Right("000" + (Convert.ToInt16(slnum) + 1), 3).ToString();
            var slnumlist = lst.FindAll(p => p.slnum == slnum1);
            if (slnumlist.Count == 0)
            {
                lst.Add(new SPEENTITY.C_01_Mer.GetOrderWithCat(coplist1.comcod, coplist1.mlccod,
                    coplist1.dayid, coplist1.styleid, coplist1.colorid,
                       coplist1.styledesc, coplist1.colordesc, coplist1.styleunit, coplist1.description,
                        coplist1.custordno, coplist1.custrefno, coplist1.packid, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, coplist1.s17, coplist1.s18, coplist1.s19,
                        coplist1.s20, coplist1.s21, coplist1.s22, coplist1.s23, coplist1.s24, coplist1.s25, coplist1.s26, coplist1.s27, coplist1.s28, coplist1.s29, coplist1.s30, coplist1.s31, coplist1.s32,
                        coplist1.s33, coplist1.s34, coplist1.s35, coplist1.s36, coplist1.s37, coplist1.s38,
                        coplist1.s39, coplist1.s40, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00,
                        0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, coplist1.p22,
                        coplist1.p23, coplist1.p24, coplist1.p25, coplist1.p26, coplist1.p27, coplist1.p28, coplist1.p29, coplist1.p30, coplist1.p31, coplist1.p32, coplist1.p33, coplist1.p34, coplist1.p35, coplist1.p36, coplist1.p37,
                        coplist1.p38, coplist1.p39, coplist1.p40, coplist1.totalqty, coplist1.colqty, coplist1.psum, slnum1));
            }

            ViewState["tblPackqty"] = lst.OrderBy(p => p.slnum).ToList();
            this.Bind_Pack_Allocation();
            this.CalculateTotalInFooter();
        }

        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < gvSalCon.Rows.Count; i++)
            //{
            //    if (((CheckBox)this.gvSalCon.HeaderRow.FindControl("chkhead")).Checked)
            //    {
            //        ((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked = true;
            //    }
            //    else
            //    {
            //        ((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked = false;
            //    }
            //}

        }

        protected void LbtnPushGrid_Click(object sender, EventArgs e)
        {

        }


        protected void gvStockDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Stockqty = Stockqty + Convert.ToDouble("0" + ((Label)e.Row.Cells[5].FindControl("DlgvqtyStock")).Text);
                double inprototal = Convert.ToDouble("0" + ((Label)e.Row.Cells[6].FindControl("DlgvInProcess")).Text);

                if (prevsize != ((Label)e.Row.Cells[2].FindControl("LblRescode")).Text)
                {
                    Stockqty = 0;
                    Stockqty = Convert.ToDouble("0" + ((Label)e.Row.Cells[5].FindControl("DlgvqtyStock")).Text);

                    prevsize = ((Label)e.Row.Cells[2].FindControl("LblRescode")).Text;
                    noRowSpan = e.Row.RowIndex;
                    ((Label)e.Row.Cells[6].FindControl("DlgvTotalStock")).Text = (prevsize == "000000000000") ? "" : "<center><b style='color:blue'>" + Stockqty.ToString() + "</b></center>";
                    ((Label)e.Row.Cells[8].FindControl("DlgvAvailabe")).Text = (prevsize == "000000000000") ? "" : " <center><b style='color:red'>" + (Stockqty - inprototal).ToString() + "</b></center>";

                }
                else
                {
                    //Increase the rowspan row count by one\

                    gv.Rows[noRowSpan].Cells[7].RowSpan = (gv.Rows[noRowSpan].Cells[7].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[7].RowSpan + 1 : gv.Rows[noRowSpan].Cells[7].RowSpan + 2;
                    gv.Rows[noRowSpan].Cells[6].RowSpan = (gv.Rows[noRowSpan].Cells[6].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[6].RowSpan + 1 : gv.Rows[noRowSpan].Cells[6].RowSpan + 2;
                    gv.Rows[noRowSpan].Cells[8].RowSpan = (gv.Rows[noRowSpan].Cells[8].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[8].RowSpan + 1 : gv.Rows[noRowSpan].Cells[8].RowSpan + 2;


                    e.Row.Controls[6].Visible = false;
                    e.Row.Controls[7].Visible = false;
                    e.Row.Controls[8].Visible = false;
                    ((Label)gv.Rows[noRowSpan].Cells[6].FindControl("DlgvTotalStock")).Text = "<center><b style='color:blue'>" + Stockqty.ToString() + "</b></center>";
                    ((Label)gv.Rows[noRowSpan].Cells[8].FindControl("DlgvAvailabe")).Text = " <center><b style='color:red'>" + (Stockqty - inprototal).ToString() + "</b></center>";

                }

            }
        }



        protected void dllorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();

            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            if (this.dllorderType.SelectedValue.Length>0)
            {
                string orderno= this.dllorderType.SelectedValue.ToString();
              
                dv1.RowFilter = ("stylecode2='" + orderno + "'");
                this.ddlmlccode.SelectedValue = dv1.ToTable().Rows[0]["mlccod"].ToString() + dv1.ToTable().Rows[0]["stylecode1"].ToString();
            }
            string mlccod1 = this.dllorderType.SelectedValue.ToString().Substring(0, 12);
            string styleid = this.dllorderType.SelectedValue.ToString().Substring(12, 12);
            string colorid = this.dllorderType.SelectedValue.ToString().Substring(24, 12);
            string dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod1, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Not Found');", true);
                return;
            }
            //  this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.txtordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");
            this.txtexpqty.Text = (Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]) - Convert.ToDouble(result.Tables[0].Rows[0]["packplanqty"])).ToString("#,##0;(#,##0); ");
            this.txtlccontact.Text = (result.Tables[0].Rows[0]["lccontactno"]).ToString() ;
            this.txtexdat.Text = Convert.ToDateTime(result.Tables[0].Rows[0]["exfactdat"]).ToString("dd-MMM-yyyy");

            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, dayid, "", "", "", "", "", "", "");
            DataTable Tempdt = ds2.Tables[0].Copy();
            DataView Tempdv = Tempdt.DefaultView;
            Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
            Tempdt = Tempdv.ToTable();
            this.GetArticleWiseCustomerPO();
            if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
            {

                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
            }
        }
        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetComeCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;



        }
        protected void gv1pack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> packlst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);

            int rowindex = (this.gv1pack.PageSize) * (this.gv1pack.PageIndex) + e.RowIndex;

            bool result = proc1.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "DELETE_PACK_LIST_ITEM", mlccod, dayid, packlst[rowindex].styleid,
                packlst[rowindex].colorid, packlst[rowindex].slnum, "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            packlst.RemoveAt(rowindex);
            ViewState["tblPackqty"] = packlst;
            this.Bind_Pack_Allocation();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.Save_Value_Packing_allocation();

            List<SPEENTITY.C_01_Mer.GetOrderWithCat> packlst = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            string mlccod = this.dllorderType.SelectedValue.ToString().Substring(0, 12);

            string styleid = this.dllorderType.SelectedValue.ToString().Substring(12, 12);
            string colorid = this.dllorderType.SelectedValue.ToString().Substring(24, 12);
            string dayid = this.dllorderType.SelectedValue.ToString().Substring(36, 8);
            string orderno = this.DdlCustorder.SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GET_ARTICLE_PO_WISE_PACKING_INFO", orderno, mlccod, dayid, styleid, colorid, "", "", "", "", "");
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>().ToList();

            if (lst == null)
                return;

            List<SPEENTITY.C_01_Mer.GetOrderWithCat> packlsst = packlst.Concat(lst).ToList();
           // packlsst[packlsst.Count() - 1].slnum = ASTUtility.Right("000" + (Convert.ToInt16(packlsst.Count())), 3).ToString();
            ViewState["tblPackqty"] = packlsst.OrderBy(p => p.styleid).OrderBy(p=>p.colorid).ToList();
            Bind_Pack_Allocation();
            this.CalculateTotalInFooter();

        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }
        private void PreviousList()
        {
            string txtdate =  Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComeCode();
            string Invno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString();
            string buyerid = this.ddlBuyer.SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_PREV_EXPORT_PLAN_NO", txtdate, Invno, buyerid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.DataTextField = "packpln1";
            this.ddlPrevList.DataValueField = "packpln";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }
        protected void gv1pack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode>)Session["lstgencode"];
                var lstpack = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "27");
                lstpack.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

                DropDownList DdlPacklist = (DropDownList)e.Row.FindControl("DdlPacklist");
                DdlPacklist.DataTextField = "gdesc";
                DdlPacklist.DataValueField = "gcod";
                DdlPacklist.DataSource = lstpack;
                DdlPacklist.DataBind();
                DdlPacklist.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "packid"));

            }
        }
        protected void LbtnShowAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string ordtype = this.dllorderType.SelectedValue.ToString();
            string style = "";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "STYLEWISE_ORDER_DETAILS", mlccod, ordtype, style, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 1].Visible = true;
                this.gvsizes.Columns[columid + 1].HeaderText = ds1.Tables[1].Rows[i]["sizedesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = ds1.Tables[0];
            this.gvsizes.DataBind();
            string Buyer = this.ddlBuyer.SelectedValue.ToString() + "%";
            string date2 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "RPT_LOCATION_WISE_STOCK", "%", date2, date2, Buyer, mlccod + "%", ordtype + "%", "Location", "", "");
            Session["tblstock"] = ds2.Tables[0];
            this.gvStockDetails.DataSource = ds2.Tables[0];
            this.gvStockDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }


        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string styleid = "";
            string colorid = "";
            string dayid = this.dllorderType.SelectedValue.ToString();
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }
            if (result.Tables[0].Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }

            this.txtordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
            DataSet result1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_EXPORT_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");
            double exportqty = 0;
            if (result1 == null)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }
            if (result1.Tables[0].Rows.Count > 0)
            {
                exportqty = Convert.ToDouble(result1.Tables[0].Rows[0]["totlprs"]);
            }
            this.txtexpqty.Text = (Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]) - exportqty).ToString("#,##0.00;(#,##0.00); ");
        }


    }
}