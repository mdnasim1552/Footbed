﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SPELIB
{   // ProcessAccess _logdata = new ProcessAccess("");
    public static class CALogRecord
    {
        //public static DataSet GetLogRecord(string Comp1, string eventtime1, string eventtime2, string SrchStr)
        //{
        //    //CAProcessAccess Proc1 = new CAProcessAccess("MSG");
          
        //    //DataSet ds1 = Proc1.GetRecords(Comp1, "SP_ENTRY_EVENTLOG", "GPWISERESBUDGET", eventtime1, eventtime2, SrchStr, "", "", "", "", "", "", "", "", "", "", "");
        //    //if (ds1 == null)
        //    //    return null;
        //    return ds1;
        //}

        public static bool AddLogRecord(string comcod, Hashtable hst,  string eventtype, string eventdesc, string eventdesc2)
        {
            string TermID = hst["compname"].ToString();
            string usrid =hst["usrid"].ToString();
            string sesionid =hst["session"].ToString();
            string eventtime = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            ProcessAccess _LogRecord = new ProcessAccess("ASTREALERPMSG");
            bool _result = _LogRecord.UpdateTransInfo(comcod, "SP_ENTRY_EVENTLOG", "ADDEVENTLOG", TermID, usrid, sesionid, eventtime, eventtype, eventdesc, eventdesc2, "", "", "", "", "", "","","");
            return _result;
        }

        //public static bool VerifyLogRecord(DataSet dsLogIn, string eventid, string verifyrmrk)
        //{
        ////    DataTable TblUsr = dsLogIn.Tables["TblUsr"]; 
        ////    string vusrid = TblUsr.Rows[0]["usrid"].ToString().Trim();
        ////    string vusrname = TblUsr.Rows[0]["usrsname"].ToString().Trim() + " [" + TblUsr.Rows[0]["usrname"].ToString().Trim() + "]";
        ////    string vsesionid = TblUsr.Rows[0]["SessionID"].ToString().Trim();
        ////    string TermID = TblUsr.Rows[0]["TermID"].ToString().Trim();
        ////    CAProcessAccess Proc1 = new CAProcessAccess("MSG");
        ////    bool _result = Proc1.UpdateRecords("XXXXXXXXXXXX", "SP_ENTRY_EVENTLOG", "VERIFYEVENTLOG", TermID, eventid, vusrid, vusrname, vsesionid, verifyrmrk, "", "", "", "", "", "", "", "");

        //return _result;
        //}


        //public static bool AddLogRecordTrnsection()
        //{

        //}
    }
}
