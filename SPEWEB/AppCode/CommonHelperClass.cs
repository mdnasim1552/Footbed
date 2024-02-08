using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
using System.Data;
using System.Web.Script.Services;



using SPELIB;
//using SPEENTITY.C_22_Sal;

/// <summary>
/// Summary description for CommonHelperClass
/// </summary>
public class CommonHelperClass
{
    //Common ObjCommon = new Common();
    ProcessAccess purdata =new ProcessAccess();
    public CommonHelperClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static CommonHelperClass Current
    {
        get
        {
            CommonHelperClass session = (CommonHelperClass)HttpContext.Current.Session["__RMGSession__"];
            if (session == null)
            {
                session = new CommonHelperClass();
                HttpContext.Current.Session["__RMGSession__"] = session;
            }
            return session;
        }
    }
    
    public DataSet tblUserMod { get; set; }
    
    public void GetAllData()
    {
    }

    public void GetCompanyData ( string comcod, string userid)
    {
        

    }

    public void GetSisterConcernInf()
    {
     

    }


    
    
}