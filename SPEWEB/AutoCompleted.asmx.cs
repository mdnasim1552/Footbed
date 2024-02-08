using SPELIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SPEWEB
{
    /// <summary>
    /// Summary description for AutoCompleted
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoCompleted : System.Web.Services.WebService
    {
        ProcessAccess MISData = new ProcessAccess();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public void GetResCode(string Comcode, string ProcedureName, string CallType, string Desc1, string Desc2, string Desc3, string Desc4, string Desc5, string Desc6, string Desc7, string Desc8, string Desc9)
        {
            DataSet ds4 = MISData.GetTransInfo(Comcode, ProcedureName, CallType, Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7, Desc8, Desc9);
            if (ds4 == null)
                return;
            Session["tblrescode"] = ds4.Tables[0];
        }
        [WebMethod]
        public void GetRecAndPayto(string Comcode, string ProcedureName, string CallType, string Desc1, string Desc2, string Desc3, string Desc4, string Desc5, string Desc6, string Desc7, string Desc8, string Desc9)
        {
            DataSet ds4 = MISData.GetTransInfo(Comcode, ProcedureName, CallType, Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7, Desc8, Desc9);
            if (ds4 == null)
                return;
            Session["tblrecandPayto"] = ds4.Tables[0];
        }
    }
}
