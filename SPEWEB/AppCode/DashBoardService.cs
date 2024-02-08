using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SPELIB;
//using SPEENTITY.C_46_GrMgtInter;
using System.Web.Script.Services;
using System.Collections;
using System.Data;


using SPEENTITY;

/// <summary>
/// Summary description for DashBoardService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DashBoardService : System.Web.Services.WebService {

    public DashBoardService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public List<SPEENTITY.C_34_Mgt.SalPurAccCombo> GetMonthlySalCal(string selectedDate)
    {

        SPEENTITY.C_34_Mgt.SalPurAccComboManager obj=new SPEENTITY.C_34_Mgt.SalPurAccComboManager();

        List<SPEENTITY.C_34_Mgt.SalPurAccCombo> lst1 = obj.GetSalPurAccByMon(selectedDate);
        return lst1;
      

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<SPEENTITY.C_34_Mgt.EClassSalPurAcc> GetMonthlySalCalLP(string selectedDate)
    {

        
        SPEENTITY.C_34_Mgt.SalPurAccComboManager obj = new SPEENTITY.C_34_Mgt.SalPurAccComboManager();

        List<SPEENTITY.C_34_Mgt.EClassSalPurAcc> lst1 = obj.GetSalPurAccByMonLP(selectedDate);
        return lst1;


    }
    
}
