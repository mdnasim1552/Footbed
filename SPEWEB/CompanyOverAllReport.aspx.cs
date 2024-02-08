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
using SPELIB;

namespace SPEWEB
{
    public partial class CompanyOverAllReport : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = date;
                ((Label)this.Master.FindControl("lblTitle")).Text = "All in One";
                // this.txtDateto.Text = Convert.ToDateTime (this.txtDateFrom.Text.Trim ()).AddMonths (1).AddDays (-1).ToString ("dd-MMM-yyyy");


                //DateTime date = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy"));
                //DateTime prevdate = date.AddMonths(-1);
                //this.txtDateFrom.Text = prevdate.ToString ("dd-MMM-yyyy");
                //this.txtDateto.Text = System.DateTime.Today.ToString ("dd-MMM-yyyy");
                // this.btnok_Click(null, null);
                ;

                Visibility();


            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string date1, string date2)
        {

            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess purData = new ProcessAccess();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_DASHBOARDMAIN", "RECEIPTPAYMETCASHFLOW", date1, date2, "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.account>();
            var lst1 = ds1.Tables[1].DataTableToList<SPEENTITY.account>();
            var lst2 = ds1.Tables[2].DataTableToList<SPEENTITY.account>();
            var lst3 = ds1.Tables[4].DataTableToList<SPEENTITY.account>();
            var lst4 = ds1.Tables[3].DataTableToList<SPEENTITY.sales>();
            var lst5 = ds1.Tables[5].DataTableToList<SPEENTITY.sales>();
            var lst6 = ds1.Tables[7].DataTableToList<SPEENTITY.sales>();
            var lst7 = ds1.Tables[6].DataTableToList<SPEENTITY.sales>();
            var lst8 = ds1.Tables[8].DataTableToList<SPEENTITY.sales>();
            var lst9 = ds1.Tables[9].DataTableToList<SPEENTITY.sales>();
            var lst10 = ds1.Tables[10].DataTableToList<SPEENTITY.account>();

            var datalist = new MyAllData(lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8, lst9, lst10);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;
        }


        private void Visibility()
        {
            string comcod = this.GetCompCode();
            this.lblaccount.Visible = true;
            this.lblsales.Visible = true;
            this.lblpurchase.Visible = true;
            this.lblcons.Visible = true;
            this.lblbbalance.Visible = true;
            this.lblstock.Visible = true;
            this.lblbill.Visible = true;
            this.lblmanpower.Visible = true;

            this.lblfcost.Visible = true;
            this.lblfunvscost.Visible = true;
            this.lblMerch.Visible = true;
            //this.hlnkDetails.Visible = comcod.Substring(0, 1) == "1";
        }
        public class MyAllData
        {
            public List<SPEENTITY.account> account { get; set; }
            public List<SPEENTITY.account> sales { get; set; }
            public List<SPEENTITY.account> purchase { get; set; }
            public List<SPEENTITY.account> tarvsact { get; set; }
            public List<SPEENTITY.sales> bankbalance { get; set; }
            public List<SPEENTITY.sales> stock { get; set; }
            public List<SPEENTITY.sales> penbil { get; set; }
            public List<SPEENTITY.sales> ffund { get; set; }
            public List<SPEENTITY.sales> fcost { get; set; }
            public List<SPEENTITY.sales> fundcost { get; set; }
            public List<SPEENTITY.account> merch { get; set; }
            public MyAllData()
            {

            }
            public MyAllData(List<SPEENTITY.account> account, List<SPEENTITY.account> sales, List<SPEENTITY.account> purchase,
                List<SPEENTITY.account> tarvsact, List<SPEENTITY.sales> bankbalance, List<SPEENTITY.sales> stock, List<SPEENTITY.sales> penbil,
                    List<SPEENTITY.sales> ffund, List<SPEENTITY.sales> fcost, List<SPEENTITY.sales> fundcost, List<SPEENTITY.account> merch)
            {
                this.account = account;
                this.sales = sales;
                this.purchase = purchase;
                this.tarvsact = tarvsact;
                this.bankbalance = bankbalance;
                this.stock = stock;
                this.penbil = penbil;
                this.ffund = ffund;
                this.fcost = fcost;
                this.fundcost = fundcost;
                this.merch = merch;

            }
        }

    }
}