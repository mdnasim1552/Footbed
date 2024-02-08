using SPEENTITY;
using SPEENTITY.C_10_Procur;
using SPEENTITY.C_15_Pro;
using SPEENTITY.C_23_MAcc;
using SPELIB;
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

namespace SPEWEB
{
    public partial class AllGraph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Visible = false;
                ((Label)this.Master.FindControl("lblprintstk")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string dates)

        {
            try
            {
                Common ObjCommon = new Common();
                string comcod = ObjCommon.GetCompCode();
                UserManOrder objUserService = new UserManOrder();
                UserManPur objUserServicePur = new UserManPur();
                BL_Production objUserServProduction = new BL_Production();
                UserDB_BL objUserServiceACC = new UserDB_BL();
                ProcessAccess _DataEntry = new ProcessAccess();
                List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, dates);//sales
                List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> list2 = objUserServicePur.ShowPurMonth(comcod, dates);// purchase
                List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> lst3 = objUserServProduction.ShowMonthly(comcod, dates); //production
                List<SPEENTITY.C_23_MAcc.EClassDB_BO.EClassAccMonthly> list4 = objUserServiceACC.ShowMonthlyAcc(dates); //acccounts
                DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", dates, "", "", "", "", "", "", "");
                List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum>();
                //   return "hello Safi"+Convert.ToDateTime(dates).ToString("dd-MM-yyyy");
                var balsheetlist = lst5.FindAll(p => p.grp == "2");
                //  return js;
                //  var result = lst1.Concat(list2);
                var datalist = new MyallData(lst1, list2, lst3, list4, balsheetlist);

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(datalist);
                //var json = jsonSerialiser.Serialize(list2);
                return json;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }

    }

    public class MyallData
    {
        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> sales { get; set; }
        public List<EClassPur.EClassMonthly> pur { get; set; }
        public List<BO_Production.EClassMonthly> prod { get; set; }
        public List<EClassDB_BO.EClassAccMonthly> acc { get; set; }
        public List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> balshet { get; set; }

        public MyallData() { }
        public MyallData(List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> sales, List<EClassPur.EClassMonthly> pur, List<BO_Production.EClassMonthly> prod, List<EClassDB_BO.EClassAccMonthly> acc, List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> balshet)
        {
            this.sales = sales;
            this.pur = pur;
            this.prod = prod;
            this.acc = acc;
            this.balshet = balshet;
        }
    }
}