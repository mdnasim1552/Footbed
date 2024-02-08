using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SPEENTITY;
using System.Web.SessionState;
using System;
using System.Data;
using SPELIB;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UserService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class UserService : System.Web.Services.WebService
{
    UserManager userManager = new UserManager();
    private static UserManagerKPI objUser = new UserManagerKPI();
    UserManGenAccount userManAcc = new UserManGenAccount();
    ProcessAccess _ProAccess = new ProcessAccess();



    [WebMethod(EnableSession = true)]
    public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHeadREQ(string actcode, string filter, string srchoption)
    {
        List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHeadREQ(actcode, filter, srchoption);

        return lst;
    }




    [WebMethod(EnableSession = true)]
    public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHead(string actcode, string filter, string srchoption)
    {
        List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHead(actcode, filter, srchoption);

        return lst;
    }

    public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> GetResHead1(string actcode, string filter, string srchoption)
    {
        List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHead1(actcode, filter, srchoption);

        return lst;
    }




    [WebMethod(EnableSession = true)]
    public List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> GetActHead(string filter, string acthead, string vounum)
    {
        List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = userManAcc.GetActHead(filter, acthead, vounum);

        return lst;
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<EClassCompInf> GetCompInf(string date)
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        //string UserId = hst["userid"].ToString();
        List<EClassCompInf> lst = userManager.ShowGetCompinf(date);
        Session["tblCompinfo"] = lst;
        return lst;
    }


    [WebMethod(EnableSession = true)]
    public List<EClassModule> GetModule(string ModuleId, string InputName)
    {
        List<EClassModule> lst = userManager.ShowModule(ModuleId, InputName);
        return lst;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<EClassComModule> GetComModule()
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        //string UserId = hst["userid"].ToString();
        List<EClassComModule> lst = userManager.ShowModule2(hst["usrid"].ToString());
        Session["tblmodule"] = lst;
        return lst;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<EclassShortCut> GetShortCut()
    {
        DataTable dt= ((DataSet)Session ["tblusrlog"]).Tables [3];      
        List<EclassShortCut> lst = dt.DataTableToList<EclassShortCut>();      
        return lst;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<EclassUserPermissionPages> GetSearchUrl(string strkeys)
    {
        DataTable dt = ((DataSet)Session ["tblusrlog"]).Tables [1];
        DataView dv = dt.DefaultView;
        dv.RowFilter = "dscrption like '%" + strkeys + "%'";
        List<EclassUserPermissionPages> lst = dv.ToTable().DataTableToList<EclassUserPermissionPages>();
        return lst;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<SPEENTITY.UserManager.userNotification> GetNotAndMessage(string userid)
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();

        List<SPEENTITY.UserManager.userNotification> lst = new List<SPEENTITY.UserManager.userNotification>();
        string todate = DateTime.Today.ToString("dd-MMM-yyyy");

        SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_NOTICE", "GETEVENTLOG", todate, userid, "", "", "", "", "", "", "");
        if (sdr == null)
        {
            return lst;
        }
        while (sdr.Read())
        {
            string url1 = "";
            if (sdr["sendphoto"] != null && sdr["sendphoto"].ToString() != "")
            {
                byte[] ifff = (byte[])sdr["sendphoto"];
                if(ifff.Length>0)
                {
                    url1 = "data:image;base64," + Convert.ToBase64String(ifff);

                }
                else
                {
                    url1 = "Content/Theme/images/avatars/human_avatar.png";

                }

            }
            else
            {
                url1 = "Content/Theme/images/avatars/human_avatar.png";
            }
                SPEENTITY.UserManager.userNotification typuser = new SPEENTITY.UserManager.userNotification(Convert.ToInt32(sdr["notifyid"].ToString()),
                sdr["meassage"].ToString(), sdr["eventitle"].ToString(), Convert.ToInt32(sdr["userid"].ToString()),
                sdr["sendname"].ToString(), url1, sdr["refid"].ToString(),
                sdr["notiytype"].ToString(), sdr["ntype"].ToString(), sdr["ncreated"].ToString());
            lst.Add(typuser);
        }
        return lst;
    }




}
