using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_03_CostABgd
{
    [Serializable]
    public class LcOrdercosting
    {
        // comcod, mgrp, groupno, mgrpdesc, groupname, mlccod, ordrid, itmno  ,itmdesc,unit, itmqty,itmrat,itmamt, rate, conamt, percntge
        public string comcod { get; set; }
        public string mgrp { get; set; }
        public string groupno { get; set; }
        public string mgrpdesc { get; set; }
        public string groupname { get; set; }
        public string mlccod { get; set; }
        public string ordrid { get; set; }
        public string itmno { get; set; }
        public string itmdesc { get; set; }
        public string unit { get; set; }
        public double itmqty { get; set; }
        public double itmrat { get; set; }
        public double itmamt { get; set; }
        public double rate { get; set; }
        public double conamt { get; set; }
        public double percntge { get; set; }
        public LcOrdercosting() { }
    }
    [Serializable]
    public class ProformaInvTrms
    {
        // comcod, mgrp, groupno, mgrpdesc, groupname, mlccod, ordrid, itmno  ,itmdesc,unit, itmqty,itmrat,itmamt, rate, conamt, percntge
        public string comcod { get; set; }
        public string termsid { get; set; }
        public string termssubj { get; set; }
        public string termsdesc { get; set; }
        public string termsrmrk { get; set; }
        public ProformaInvTrms() { }
    }
    [Serializable]
    public class EclassSalesContact{
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string artno { get; set; }
        public string custrefno { get; set; }
        public string styleid { get; set; }
        public string styledesc { get; set; }
        public string colorid { get; set; }
        public string colordesc { get; set; }
        public string sizeid { get; set; }
        public string sizedesc { get; set; }
        public double ordrqty { get; set; }       
        public DateTime delvdat { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public double ordrqty1 { get; set; }
        public double rate { get; set; }
        public string ordrno { get; set; }
        public string po { get; set; }
        public string custordno { get; set; }
        public string hscode { get; set; }
        public string style1 { get; set; }
        public string styledesc1 { get; set; }
        public double balqty { get; set; }
        public string rdayid { get; set; }
        public string rdaydesc { get; set; }
        public string custname { get; set; }
        public string custdetails1 { get; set; }
        public string custdetails2 { get; set; }
        public string sdino { get; set; }
        public string lastformadesc { get; set; }
        public double inprocqty { get; set; }
        public double sizewisetotal { get; set; }
        public string cursymbol { get; set; }
        public string curword { get; set; }
        public string curdesc { get; set; }
        public string subcurdesc { get; set; }
        public string codedesc { get; set; }
        public double totlctn { get; set; }
        public double totlprs { get; set; }
        public string consigne { get; set; }
        public EclassSalesContact() { }
        public EclassSalesContact( string mlccod, string mlcdesc, string artno, string styleid, string styledesc, string colorid, string colordesc,
           string sizeid, string sizedesc, double ordrqty, DateTime delvdat, double ordrqty1, double rate, string ordrno, string hscode, 
           string rdayid, string rdaydesc, string custordno, string lastformadesc, string custrefno)
        {

            this.mlccod = mlccod;
            this.mlcdesc = mlcdesc;
            this.artno = artno;
            this.styleid = styleid;
            this.styledesc = styledesc;
            this.colorid = colorid;
            this.colordesc = colordesc;
            this.sizeid = sizeid;
            this.sizedesc = sizedesc;
            this.ordrqty = ordrqty;
            this.delvdat = delvdat;
            this.ordrqty1 = ordrqty1;
            this.rate = rate;
            this.ordrno = ordrno;
            this.hscode = hscode;
            this.rdayid = rdayid;
            this.rdaydesc = rdaydesc;
            this.custordno = custordno;
            this.lastformadesc = lastformadesc;
            this.custrefno = custrefno;
        }
    }
    [Serializable]
    public class EcLassAttachMent
    {
        public string mlccod { get; set; }
        public string styleid { get; set; }
        public int id { get; set; }
        public string attchurl { get; set; }
        public DateTime  posteddat { get; set; }
        public string postedbyid { get; set; }
        public EcLassAttachMent() { }
        public EcLassAttachMent(string mlccod, string styleid, int id, string attchurl, DateTime posteddat, string postedbyid) {
            this.mlccod = mlccod;
            this.styleid = styleid;
            this.id = id;
            this.attchurl = attchurl;
            this.posteddat = posteddat;
            this.postedbyid = postedbyid;
        }

    }
}






