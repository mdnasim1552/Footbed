using SPELIB;
using SPEENTITY.C_22_Sal;
using SPEENTITY.C_10_Procur;
using SPEENTITY.C_15_Pro;
using SPEENTITY.C_21_Acc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_31_Mis
{
    public partial class RptAllDashboard : System.Web.UI.Page
    {
        ProcessAccess _DataEntry = new ProcessAccess();
        static string prevPage = String.Empty;
        UserManSales objUserService = new UserManSales();
        UserManPur objUserServicePur = new UserManPur();
        UserDB_BL objUserServiceACC = new UserDB_BL();
        BL_Production objproduction = new BL_Production();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{
                //    prevPage = Request.UrlReferrer.ToString();
                //}
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.txtCurTransDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ExRelz") ? "Export & Realization Analysis" : (type == "Merchandiser") ? "Merchandising Analysis" :
                    (type == "Purchase") ? "Purchase Analysis" : (type == "Accounts") ? "Accounts Analysis" : (type == "Production" || type == "ProductionRMG") ? "Production Analysis" : "";
                //this.SelectView();
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "pageLoaded1()", true);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        public void SelectView()
        {
            string type = this.Request.QueryString["Type"].Trim().ToString();
            switch (type)
            {
                case "ExRelz":
                    GetSalesData();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "Purchase":
                    GetPurchaseData();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "Accounts":
                    GetAccountsData();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "Production":
                case "ProductionRMG":
                    GetProductionData();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "Merchandiser":
                    GetMerchandiserData();
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
            }

        }
        public void GetSalesData()
        {

            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ALL_DASHBOARD", "SALESDASHBOARD_INFO", dates, "", "", "", "", "", "", "");
            List<SPEENTITY.C_22_Sal.EClassDashboardList> lst5 = ds2.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EClassDashboardList> todaysal = ds2.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EClassDashboardList> wksal = ds2.Tables[2].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EClassDashboardList> salperf = ds2.Tables[3].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EClassDashboardList> divwise = ds2.Tables[4].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EClassDashboardList> custretailser = ds2.Tables[5].DataTableToList<SPEENTITY.C_22_Sal.EClassDashboardList>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topcustmon = ds2.Tables[6].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topprodmon = ds2.Tables[7].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topteam = ds2.Tables[8].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topteamweek = ds2.Tables[9].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();

            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> monthsal = objUserService.ShowMonthly(comcod, dates);
            var list = lst5.Concat(todaysal).Concat(wksal).Concat(salperf).Concat(custretailser).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            var json2 = jsonSerialiser.Serialize(divwise);
            var json3 = jsonSerialiser.Serialize(monthsal);
            var toplist = topcustmon.Concat(topprodmon).Concat(topteam).Concat(topteamweek).ToList();
            var json4 = jsonSerialiser.Serialize(toplist);
            //return json;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteSalesGraph('" + json + "','" + json2 + "','" + json3 + "','" + json4 + "')", true);


        }
        public void GetPurchaseData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            string month = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("MMM");
            List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> purmonth = objUserServicePur.ShowPurMonth(comcod, dates);
            List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> curmonth = purmonth.FindAll(p => p.yearmon1 == month);
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ALL_DASHBOARD", "PURCHASE_DASHBOARD_INFO", dates, "", "", "", "", "", "", "");
            List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> weeklypur = ds2.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topsuppur = ds2.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topmat = ds2.Tables[2].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topsupout = ds2.Tables[3].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();
            List<SPEENTITY.C_22_Sal.EclassCustomerProductSumary> topsuppay = ds2.Tables[4].DataTableToList<SPEENTITY.C_22_Sal.EclassCustomerProductSumary>();

            var monthly = purmonth.Concat(curmonth).Concat(weeklypur).ToList();
            var top5data = topsuppur.Concat(topmat).Concat(topsupout).Concat(topsuppay).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var pur_json = jsonSerialiser.Serialize(monthly);
            var pur_json1 = jsonSerialiser.Serialize(top5data);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecutePurchaseGraph('" + pur_json + "','" + pur_json1 + "')", true);

        }
        public void GetAccountsData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            List<EClassDB_BO.EClassAccMonthly> monthacc = objUserServiceACC.ShowMonthlyAcc(comcod, dates);
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", dates, "", "", "", "", "", "", "");
            List<SPEENTITY.C_31_Mis.UserManageMis.EclassBalSheetSum> balsheet = ds2.Tables[0].DataTableToList<SPEENTITY.C_31_Mis.UserManageMis.EclassBalSheetSum>();
            DataSet accds = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ALL_DASHBOARD", "ACC_DASHBOARD_INFO", dates, "", "", "", "", "", "", "");
            var curbalnce = accds.Tables[0].DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.EclassOverallBalance>();
            var todayreceive = accds.Tables[1].DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.EclassOverallBalance>();
            var todaypay = accds.Tables[2].DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.EclassOverallBalance>();
            var monthlyrec = accds.Tables[3].DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.EclassOverallBalance>();
            var monthpay = accds.Tables[4].DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.EclassOverallBalance>();
            var curmonth = monthacc.FindAll(s => s.yearmon1 == Convert.ToDateTime(dates).ToString("MMM"));
            var balsheetlist = balsheet.FindAll(p => p.grp == "2");
            var jsonSerialiser = new JavaScriptSerializer();
            var bal_json = jsonSerialiser.Serialize(balsheetlist);
            var acc_json = jsonSerialiser.Serialize(monthacc.Concat(curmonth));
            var curbl_json = jsonSerialiser.Serialize(curbalnce);
            var todayreceive_json = jsonSerialiser.Serialize(todayreceive);
            var todaypay_json = jsonSerialiser.Serialize(todaypay);
            var monthrecpay_json = jsonSerialiser.Serialize(monthlyrec);
            var monthpay_json = jsonSerialiser.Serialize(monthpay);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteAccGraph('" + bal_json + "','" + acc_json + "','" + curbl_json + "','" + todayreceive_json + "','" + todaypay_json + "','" + monthrecpay_json + "','" + monthpay_json + "')", true);

        }

        public void GetProductionData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            List<BO_Production.EClassMonthly> lst3 = objproduction.ShowMonthly(comcod, dates);
            DataSet prods = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ALL_DASHBOARD", "PRODUCTION_DASHBOARD_INFO", dates, "", "", "", "", "", "", "");
            var mostitem = prods.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.Day_wise_Production>();
            var gailoss = prods.Tables[1].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdGainLoss>();
            var jsonSerialiser = new JavaScriptSerializer();
            var curmonth = lst3.FindAll(s => s.yearmon1 == Convert.ToDateTime(dates).ToString("MMM"));
            var year_prod = jsonSerialiser.Serialize(lst3.Concat(curmonth));
            var mostitm_json = jsonSerialiser.Serialize(mostitem);
            var gailoss_json = jsonSerialiser.Serialize(gailoss);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteProductionGrpah('" + year_prod + "','" + mostitm_json + "','" + gailoss_json + "')", true);

        }
        public void GetMerchandiserData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            DataSet prods = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ALL_DASHBOARD", "MARCHAND_ANALYSIS_INFO", dates, "", "", "", "", "", "", "");
            var monthwise = prods.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassMonthlyMerchandStatus>();
            var jsonSerialiser = new JavaScriptSerializer();
            var monthwise_json = jsonSerialiser.Serialize(monthwise);
            //   ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Test()", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Test()", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteMerchandGraph('" + monthwise_json + "')", true);

        }
        protected void OkBtn_Click(object sender, EventArgs e)
        {
            this.SelectView();
            this.divInitialCard.Visible = false;
        }
    }

}