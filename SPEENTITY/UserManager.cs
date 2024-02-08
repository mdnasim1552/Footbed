using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using  SPELIB;


namespace SPEENTITY
{

    public class UserManager : System.Web.UI.Page
    {

        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();





        public List<SPEENTITY.C_07_Fin.EClassPayment> GetPayInfo(string Procedure, string Calltype, string Date1, string Date2)
        {
            List<SPEENTITY.C_07_Fin.EClassPayment> lst = new List<SPEENTITY.C_07_Fin.EClassPayment>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, Date1, Date2, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_07_Fin.EClassPayment details = new SPEENTITY.C_07_Fin.EClassPayment(dr["mlccod"].ToString(), dr["mlcdesc"].ToString(), dr["blccod"].ToString(),
                        dr["blcdesc"].ToString(), (dr["suplrid"]).ToString(), dr["supldesc"].ToString(), Convert.ToDateTime(dr["suppldate"]), Convert.ToDateTime(dr["setdate"]),
                        Convert.ToDouble(dr["amt1"]), Convert.ToDouble(dr["amt2"]), Convert.ToDouble(dr["amt3"]), Convert.ToDouble(dr["amt4"]), Convert.ToDouble(dr["amt5"]),
                        Convert.ToDouble(dr["amt6"]), Convert.ToDouble(dr["toamt"]));
                lst.Add(details);
            }

            return lst;

        }

        public List<SPEENTITY.C_07_Fin.EClassCollection> GetCollInfo(string Procedure, string Calltype, string Date1, string Date2)
        {
            List<SPEENTITY.C_07_Fin.EClassCollection> lst = new List<SPEENTITY.C_07_Fin.EClassCollection>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, Date1, Date2, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_07_Fin.EClassCollection details = new SPEENTITY.C_07_Fin.EClassCollection(dr["mlccod1"].ToString(), dr["mlcdesc1"].ToString(), dr["mlccod"].ToString(),
                        dr["mlcdesc"].ToString(), (dr["invno"]).ToString(), Convert.ToDateTime(dr["shipmdat"]), Convert.ToDateTime(dr["settldat"]),
                        Convert.ToDouble(dr["amt1"]), Convert.ToDouble(dr["amt2"]), Convert.ToDouble(dr["amt3"]), Convert.ToDouble(dr["amt4"]), Convert.ToDouble(dr["amt5"]),
                        Convert.ToDouble(dr["amt6"]), Convert.ToDouble(dr["toamt"]));
                lst.Add(details);
            }

            return lst;

        }


        public List<SPEENTITY.C_07_Fin.EClassCollVsPayment> GetCollVsPay(string Procedure, string Calltype, string Date1, string Date2)
        {
            List<SPEENTITY.C_07_Fin.EClassCollVsPayment> lst = new List<SPEENTITY.C_07_Fin.EClassCollVsPayment>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, Date1, Date2, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_07_Fin.EClassCollVsPayment details = new SPEENTITY.C_07_Fin.EClassCollVsPayment(dr["grp"].ToString(), dr["grpdesc"].ToString(),
                        dr["code1"].ToString(), dr["code2"].ToString(), dr["code3"].ToString(), dr["desc1"].ToString(), dr["desc2"].ToString(), dr["desc3"].ToString(),
                        Convert.ToDateTime(dr["date1"]), Convert.ToDateTime(dr["settldat"]), Convert.ToDouble(dr["amt1"]), Convert.ToDouble(dr["amt2"]),
                        Convert.ToDouble(dr["amt3"]), Convert.ToDouble(dr["amt4"]), Convert.ToDouble(dr["amt5"]), Convert.ToDouble(dr["amt6"]), Convert.ToDouble(dr["toamt"]));
                lst.Add(details);
            }

            return lst;

        }





        //public List<EClassModule> ShowModule(string Moduleid, string Inputtype)
        //{
        //    List<EClassModule> lst = new List<EClassModule>();
        //    DataTable dt = ConstantInfo.MenuTable(Moduleid);
        //    DataView dv = dt.DefaultView;
        //    dv.RowFilter = ("itemcod like '" + Inputtype + "'");
        //    DataTable dt1 = dv.ToTable();

        //    foreach (DataRow dr in dt1.Rows)
        //    {

        //        EClassModule details = new EClassModule(dr["itemcod"].ToString(), dr["itemdesc"].ToString(), dr["itemurl"].ToString(), Convert.ToBoolean(dr["itemslct"].ToString()), dr["fbold"].ToString());
        //        lst.Add(details);
        //    }
        //    return lst;

        //}
        private void CreateTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("formid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemdesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemurl", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemtips", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemslct", Type.GetType("System.Boolean"));
            mnuTbl1.Columns.Add("fbold", Type.GetType("System.String"));
            Session["tblpageinfo"] = mnuTbl1;

        }

        public List<EClassModule> ShowModule(string Moduleid, string Inputtype)
        {
            this.CreateTable();
            List<EClassModule> lst = new List<EClassModule>();
            DataTable dt = ConstantInfo.MenuTable(Moduleid);

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcod like '" + Inputtype + "'");
            DataTable dt1 = dv.ToTable();

            DataTable dtdb = ((DataSet)Session["tblusrlog"]).Tables[1];

            DataTable dtpage = (DataTable)Session["tblpageinfo"];
            int i = 1;
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                string frmname = dt1.Rows[j]["itemurl"].ToString();
                Boolean itemslct = Convert.ToBoolean(dt1.Rows[j]["itemslct"].ToString());
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";
                frmname = frmname.Replace("&comcod=", "");
                frmname = frmname.Replace("&empid=", "");
                frmname = frmname.Replace("&actcode=", "");
                frmname = frmname.Replace("&genno=", "");
                frmname = frmname.Replace("&vounum=", "");
                frmname = frmname.Replace("&sircode=", "");
                frmname = frmname.Replace("&date=", "");
                frmname = frmname.Replace("&centrid=", "");
                 frmname = frmname.Replace("&reptype=NORMAL", "");            
                frmname = frmname.Replace("&reptype=", "");
                frmname = frmname.Replace("&emptype=", "");
                frmname = frmname.Replace("&dayid=", "");


                DataRow[] dr1 = dtdb.Select("(frmname+qrytype)='" + frmname + "'");

                if (dr1.Length > 0)
                {
                    string url = dt1.Rows[j]["itemurl"].ToString().Substring(2);
                    DataRow dr2 = dtpage.NewRow();
                    dr2["itemcod"] = dt1.Rows[j]["itemcod"].ToString();
                    dr2["itemdesc"] = ASTUtility.Right("00" + i.ToString(), 2) + ". " + dt1.Rows[j]["itemdesc"].ToString().Substring(3);
                    dr2["itemurl"] = dt1.Rows[j]["itemurl"].ToString();
                    dr2["itemtips"] = dt1.Rows[j]["itemtips"].ToString();
                    dr2["itemslct"] = Convert.ToBoolean(dt1.Rows[j]["itemslct"]).ToString();
                    dr2["fbold"] = dt1.Rows[j]["fbold"].ToString();
                    dr2 ["formid"] = dr1[0]["frmid"].ToString();
                    dtpage.Rows.Add(dr2);
                    i++;

                }
                else if (itemslct == false)
                {
                    DataRow dr2 = dtpage.NewRow();
                    dr2["itemcod"] = dt1.Rows[j]["itemcod"].ToString();
                    dr2["itemdesc"] = dt1.Rows[j]["itemdesc"].ToString();
                    dr2["itemurl"] = dt1.Rows[j]["itemurl"].ToString();
                    dr2["itemtips"] = dt1.Rows[j]["itemtips"].ToString();
                    dr2["itemslct"] = Convert.ToBoolean(dt1.Rows[j]["itemslct"]).ToString();
                    dr2["fbold"] = dt1.Rows[j]["fbold"].ToString();
                    dr2["formid"] = "";
                    dtpage.Rows.Add(dr2);

                }


            }

            foreach (DataRow dr in dtpage.Rows)
            {

                EClassModule details = new EClassModule(dr["itemcod"].ToString(), dr["itemdesc"].ToString(), dr["itemurl"].ToString(), Convert.ToBoolean(dr["itemslct"].ToString()), dr["fbold"].ToString(), dr["formid"].ToString().Trim());
                lst.Add(details);
            }
            return lst;

        } 
        public List<EClassComModule> ShowModule2(string UserId)
        {

            List<EClassComModule> lst = new List<EClassComModule>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_MGT", "GETMODULELISTAll", UserId, "", "", "", "", "", "", "", "");
                while (dr.Read())
            {
                EClassComModule details = new EClassComModule(dr["moduleid"].ToString(), dr["modulename"].ToString(), dr["url"].ToString());
                lst.Add(details);
            }

            return lst;


        }
      
        #region KPI
        public List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEvagen> GetEmpMonEvagen(string empid, string frmdate, string todate)
        {
            List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEvagen> lst = new List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEvagen>();
            string comcod = ObjCommon.GetCompCode();
            string deptcode = ObjCommon.GetDeptCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI03", "GRAPHMONWISE", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_47_Kpi.EClassEmployeeMonEvagen details = new SPEENTITY.C_47_Kpi.EClassEmployeeMonEvagen(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tmark"].ToString()), Convert.ToDouble(dr["acmark"].ToString()), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["avgmark"].ToString()), dr["gpa"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva(string empid, string frmdate, string todate)
        {
            List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva> lst = new List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva>();
            string comcod = ObjCommon.GetCompCode();
            string deptcode = ObjCommon.GetDeptCode();
            // string CallType = (deptcode == "010100101001") ? "SHOWMONWISEEVA" : "GRAPHMONWISE";
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVA", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_47_Kpi.EClassEmployeeMonEva details = new SPEENTITY.C_47_Kpi.EClassEmployeeMonEva(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tamt1"].ToString()), Convert.ToDouble(dr["tamt2"].ToString()), Convert.ToDouble(dr["tamt3"].ToString()), Convert.ToDouble(dr["tamt4"].ToString()),
                        Convert.ToDouble(dr["tamt5"].ToString()), Convert.ToDouble(dr["tamt6"].ToString()), Convert.ToDouble(dr["amt1"].ToString()), Convert.ToDouble(dr["amt2"].ToString()),
                        Convert.ToDouble(dr["amt3"].ToString()), Convert.ToDouble(dr["amt4"].ToString()), Convert.ToDouble(dr["amt5"].ToString()), Convert.ToDouble(dr["amt6"].ToString()),
                        Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["avgmark"].ToString()));
                lst.Add(details);
            }

            return lst;
        }



        public List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva02> GetEmpMonEva02(string empid, string frmdate, string todate, string type)
        {
            List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva02> lst = new List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva02>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVA02", empid, frmdate, todate, type, "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_47_Kpi.EClassEmployeeMonEva02 details = new SPEENTITY.C_47_Kpi.EClassEmployeeMonEva02(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["cumtar"].ToString()), Convert.ToDouble(dr["act"].ToString()), Convert.ToDouble(dr["cumact"].ToString()), Convert.ToDouble(dr["tper"].ToString()),
                        Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        public List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva04(string empid, string frmdate, string todate, string deptcode)
        {
            List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva> lst = new List<SPEENTITY.C_47_Kpi.EClassEmployeeMonEva>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVACR", empid, frmdate, todate, deptcode, "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_47_Kpi.EClassEmployeeMonEva details = new SPEENTITY.C_47_Kpi.EClassEmployeeMonEva(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tamt1"].ToString()), Convert.ToDouble(dr["tamt2"].ToString()), Convert.ToDouble(dr["tamt3"].ToString()), Convert.ToDouble(dr["tamt4"].ToString()),
                        Convert.ToDouble(dr["tamt5"].ToString()), Convert.ToDouble(dr["tamt6"].ToString()), Convert.ToDouble(dr["amt1"].ToString()), Convert.ToDouble(dr["amt2"].ToString()),
                        Convert.ToDouble(dr["amt3"].ToString()), Convert.ToDouble(dr["amt4"].ToString()), Convert.ToDouble(dr["amt5"].ToString()), Convert.ToDouble(dr["amt6"].ToString()),
                        Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["Actual"].ToString()));
                lst.Add(details);
            }

            return lst;
        }


        public List<SPEENTITY.C_47_Kpi.EClassEmpHistory> GetEmpHistory(string empid, string frmdate, string todate)
        {
            List<SPEENTITY.C_47_Kpi.EClassEmpHistory> lst = new List<SPEENTITY.C_47_Kpi.EClassEmpHistory>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTEMPPROWISEHISTORY", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_47_Kpi.EClassEmpHistory details = new SPEENTITY.C_47_Kpi.EClassEmpHistory(dr["pactdesc"].ToString(), dr["actdesc"].ToString(),
                        Convert.ToDouble(dr["duration"].ToString()), Convert.ToDouble(dr["aduration"].ToString()), Convert.ToDouble(dr["deloadv"].ToString()), dr["deloadvsign"].ToString());
                lst.Add(details);
            }

            return lst;
        }


        #endregion

        public List<EClassCompInf> ShowGetCompinf(string date)
        {

            List<EClassCompInf> lst = new List<EClassCompInf>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETGROUPATTENDENCE", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                EClassCompInf details = new EClassCompInf(dr["comcod"].ToString(), dr["comnam"].ToString(), Convert.ToDouble(dr["ttlstap"].ToString()),
                  Convert.ToDouble(dr["present"].ToString()), Convert.ToDouble(dr["late"].ToString()), Convert.ToDouble(dr["earlyLev"].ToString()), Convert.ToDouble(dr["onlev"].ToString()),
                  Convert.ToDouble(dr["absnt"].ToString()));
                lst.Add(details);
            }

            return lst;


        }
        public List<SPEENTITY.C_21_Acc.EClassSpecification> ShowSpecification(string Procedure, string Calltype, string Desc1)
        {
            List<SPEENTITY.C_21_Acc.EClassSpecification> lst = new List<C_21_Acc.EClassSpecification>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, Desc1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_21_Acc.EClassSpecification details = 
                    new SPEENTITY.C_21_Acc.EClassSpecification(
                        dr["spcfcod"].ToString(), dr["spcfcod2"].ToString(), dr["spcfcod3"].ToString(), dr["spcfcod4"].ToString(), 
                        dr["sirtdes"].ToString(), dr["spcfdesc"].ToString(), dr["desc1"].ToString(), dr["desc2"].ToString(),
                        dr["desc3"].ToString(), dr["desc4"].ToString(), dr["desc5"].ToString(), dr["unit"].ToString(), 
                        dr["incoterms"].ToString(), dr["incotermsdesc"].ToString(), Convert.ToDouble(dr["mark"]),
                        Convert.ToDouble(dr["allowance"]), Convert.ToBoolean(dr["sizeble"]), Convert.ToBoolean(dr["convertible"]), Convert.ToDouble(dr["sirval"]),
                        dr["photo"].ToString()
                    );
                lst.Add(details);
            }

            return lst;

        }


        public List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> GetLastRetNo(string Date, string Centrid)
        {
            List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_EXPORT", "LASTRETNO", Date, Centrid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet LastRet = new SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet(dr["maxno"].ToString(), dr["maxno1"].ToString());
                lst.Add(LastRet);
            }

            return lst;
        }

        public List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet> GetPreReturn(string Centrid, string Date, string Date2)
        {
            List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet> lst = new List<SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_EXPORT", "GETPRERETURN", Centrid, Date, Date2, "", "", "", "", "", "");
            while (dr.Read())
            {
                SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet details = new SPEENTITY.C_22_Sal.EClassReturn.EClassPreRet(dr["retmemo"].ToString(), dr["retmemo1"].ToString(),
                        dr["custcode"].ToString(), dr["custdesc"].ToString(), dr["invno"].ToString(), dr["invno1"].ToString(), dr["orderno"].ToString(), dr["teamcode"].ToString(), dr["teamdesc"].ToString());
                lst.Add(details);
            }

            return lst;



        }

        [Serializable]
        public class userNotification
        {
            public int notifyid { get; set; }
            public string meassage { get; set; }
            public string eventitle { get; set; }
            public int userid { get; set; }
            public string sendname { get; set; }
            public string sendphoto { get; set; }
            public string refid { get; set; }
            public string notiytype { get; set; }
            public string ntype { get; set; }
            public string ncreated { get; set; }
            public userNotification()
            {

            }
            public userNotification(int notifyid, string meassage,
                string eventitle, int userid, string sendname, string sendphoto,
                string refid, string notiytype, string ntype, string ncreated )
            {
                this.notifyid = notifyid;
                this.meassage = meassage;
                this.eventitle = eventitle;
                this.userid = userid;
                this.sendname = sendname;
                this.sendphoto = sendphoto;
                this.refid = refid;
                this.notiytype = notiytype;
                this.ntype = ntype;
                this.ncreated = ncreated;
            }
        }
    }

 
       
}
