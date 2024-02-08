using SPELIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY
{
    public class Xml_BO_Class
    {
        Common ObjCommon = new Common();

        public DataSet GetDataSetForXml(DataSet ds1, string centrid, string memono)
        {
            DataTable dtt2 = ds1.Tables[2].Copy();
            DataTable dtm = ds1.Tables[1].Copy();
            DataRow[] dr1 = dtm.Select("actcode='" + centrid + "' and memono='" + memono + "'");
            dr1[0]["postedbyid"] = ObjCommon.GetUserCode();
            dr1[0]["postrmid"] = ObjCommon.Terminal();
            dr1[0]["postseson"] = ObjCommon.Sessionid();
            dr1[0]["posteddat"] = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1xml = new DataSet("dsv1");
            DataTable tbl1 = ds1.Tables[0].Copy();
            tbl1.TableName = "tbla";
            dtm.TableName = "tblb";
            dtt2.TableName = "tblc";
            ds1xml.Tables.Add(tbl1);
            ds1xml.Tables.Add(dtm);
            ds1xml.Tables.Add(dtt2);
            return ds1xml;
        }

        public DataSet GetDataSetForXmlDo(DataSet ds1, string centrid, string memono)
        {
            DataTable dtt3 = ds1.Tables[3].Copy();
            DataTable dtt2 = ds1.Tables[2].Copy();
            DataTable dtm = ds1.Tables[1].Copy();
            DataRow[] dr1 = dtm.Select("actcode='" + centrid + "' and memono='" + memono + "'");
            dr1[0]["postedbyid"] = ObjCommon.GetUserCode();
            dr1[0]["postrmid"] = ObjCommon.Terminal();
            dr1[0]["postseson"] = ObjCommon.Sessionid();
            dr1[0]["posteddat"] = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1xml = new DataSet("dsv1");
            DataTable tbl1 = ds1.Tables[0].Copy();
            tbl1.TableName = "tbla";
            dtm.TableName = "tblb";
            dtt2.TableName = "tblc";
            dtt3.TableName = "tbld";
            ds1xml.Tables.Add(tbl1);
            ds1xml.Tables.Add(dtm);
            ds1xml.Tables.Add(dtt2);
            ds1xml.Tables.Add(dtt3);
            return ds1xml;
        }
    }
}
