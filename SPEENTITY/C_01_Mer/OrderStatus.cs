using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_01_Mer
{
    [Serializable]
    public class OrderStatus
    {
        public string comcod { get; set; }
        public string ordrid { get; set; }
        public DateTime ordrdate { get; set; }
        public double revenue { get; set; }
        public double cost { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string infgrp { get; set; }
        public double infval { get; set; }
        public string curncy { get; set; }
        public double cm { get; set; }
        public double qty { get; set; }
        public string buyer { get; set; }
        public string buyaddress { get; set; }
        public double cmdd { get; set; }
        public string qtstatus { get; set; }
        public string lcdesc { get; set; }
        public OrderStatus() { }
    }
    [Serializable]
    public class LCAnalysis
    {
        public string comcod { get; set; }
        public string infcod { get; set; }
        public string infdesc { get; set; }
        public string unit { get; set; }
        public string grpid { get; set; }
        public string grp { get; set; }
        public double qty { get; set; }
        public double rate { get; set; }
        public double amt { get; set; }
        public string supplier { get; set; }
        public double percntge { get; set; }
        public LCAnalysis() { }
    }
    [Serializable]
    public class EclassSampleInquiry
    {
        public string comcod { get; set; }
        public string autoartcle { get; set; }
        public string styleid { get; set; }
        public string styledesc { get; set; }
        public string sirunit { get; set; }
        public string category { get; set; }
        public string catedesc { get; set; }
        public string artno { get; set; }
        public string color { get; set; }
        public string sizernge { get; set; }
        public string samsize { get; set; }
        public string consize { get; set; }
        public string images { get; set; }
        public string attchmnt { get; set; }
        public string xmldata { get; set; }
        public double ordqty { get; set; }
        public string season { get; set; }
        public string seasondesc { get; set; }
        public string brand { get; set; }
        public string brandesc { get; set; }
        public EclassSampleInquiry() { }
        public EclassSampleInquiry(string comcod, string styleid, string styledesc, string sirunit, string category, string catedesc,
            string artno, string color, string sizernge, string samsize, string consize, string images, string attchmnt, double ordqty,
            string season, string seasondesc, string autoartcle, string brand, string brandesc)
        {
            this.comcod = comcod;
            this.styleid = styleid;
            this.styledesc = styledesc;
            this.sirunit = sirunit;
            this.category = category;
            this.catedesc = catedesc;
            this.artno = artno;
            this.color = color;
            this.sizernge = sizernge;
            this.samsize = samsize;
            this.consize = consize;
            this.images = images;
            this.attchmnt = attchmnt;
            this.ordqty = ordqty;
            this.season = season;
            this.seasondesc = seasondesc;
            this.autoartcle = autoartcle;
            this.brand = brand;
            this.brandesc = brandesc;

        }

    }
    [Serializable]
    public class EclassOrderDetails
    {
        public string colorid { get; set; }
        public string colordesc { get; set; }
        public string colorselect { get; set; }
        public string sizeid { get; set; }
        public string sizedesc { get; set; }
        public string sizeselect { get; set; }
        public double ordqty { get; set; }
        public EclassOrderDetails() { }
        public EclassOrderDetails(string colorid, string colordesc, string colorselect, string sizeid, string sizedesc, string sizeselect)
        {
            this.colorid = colorid;
            this.colordesc = colordesc;
            this.colorselect = colorselect;
            this.sizeid = sizeid;
            this.sizedesc = sizedesc;
            this.sizeselect = sizeselect;
        }

    }


    [Serializable]

    public class EClassSampleInq
    {
        public string comcod { get; set; }
        public string inqno { get; set; }
        public string inqno1 { get; set; }
        public string inqno2 { get; set; }
        public string buyerid { get; set; }
        public string buyerdesc { get; set; }
        public DateTime inqdat { get; set; }
        public string artno { get; set; }
        public double ordqty { get; set; }
        public string itmcount { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string category { get; set; }
        public string catedesc { get; set; }
        public string samsize { get; set; }
        public string consize { get; set; }
        public string sizernge { get; set; }
        public string images { get; set; }
        public string attchmnt { get; set; }
    }
    public class EClassCostingSummary
    {
        public string comcod { get; set; }
        public string inqno { get; set; }
        public string styleid { get; set; }
        public string curcod { get; set; }
        public double cnfrmprice { get; set; }
        public double offprice { get; set; }
        public string notes { get; set; }
        public string uppercom { get; set; }
        public string lining { get; set; }
        public string socks { get; set; }
        public string outsole { get; set; }
        public double stdamt { get; set; }
        public double outsdning { get; set; }
        public double matcost { get; set; }
        public string article { get; set; }
        public string imgurl { get; set; }
        public double agntcomm { get; set; }
        public double dsigncomm { get; set; }
        public double moccasin { get; set; }
        public double noclaim { get; set; }
        public double testfee { get; set; }
        public double locost { get; set; }
        public double devcost { get; set; }
        public double moldcost { get; set; }
        public double netcost { get; set; }
        public double ttlamt { get; set; }
        public double ttlamteuro { get; set; }
        public double profitloss { get; set; }
        public double prftlssprcnt { get; set; }
        public double othercomm { get; set; }
        public string llast { get; set; }
        public string deliverydate { get; set; }
        public string knife { get; set; }
        public string sampleconfirm { get; set; }

    }
    [Serializable]

    //a.comcod,  a.procode, rescode=a.rsircode,  a.colorid, a.sizeid, a.conqty, a.westpc, qty=a.rstdqty , amt=a.stdamt, rate=(case when a.rstdqty=0 then 0 else  a.stdamt/a.rstdqty end),
    //a.percnt, prodesc=isnull(c.SIRDESC,''), resdesc =a.rsirdesc, spcfdesc=isnull(d.spcfdesc,''), compname=isnull(b.sirdesc,'') , resunit=a.runit, a.spcfcode, a.compcode, convrate=a.rstdqty* a.conrate
    public class EclassConsumption
    {
        public string comcod { get; set; }
        public string procode { get; set; }
        public string rescode { get; set; }
        public string colorid { get; set; }
        public string sizeid { get; set; }
        public double conqty { get; set; }
        public double westpc { get; set; }
        public double qty { get; set; }
        public double amt { get; set; }
        public double rate { get; set; }
        public double percnt { get; set; }
        public string prodesc { get; set; }
        public string resdesc { get; set; }
        public string spcfdesc { get; set; }
        public string spcfdesc1 { get; set; }
        public string spcfdesccolor { get; set; }
        public string compname { get; set; }
        public string resunit { get; set; }
        public string spcfcode { get; set; }
        public string compcode { get; set; }
        public double convrate { get; set; }
        public double cfrate { get; set; }
        public double fcfrate { get; set; }
        public string compgrpcode { get; set; }
        public string compgrpdesc { get; set; }

    }

    public class EclassConsumptionFB
    {
        public string comcod { get; set; }
        public string sampleid { get; set; }
        public string grpid { get; set; }
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string rescode { get; set; }
        public double conqty { get; set; }
        public double preqty { get; set; }
        public double issueqty { get; set; }
        public double amt { get; set; }
        public double rate { get; set; }
        public double percnt { get; set; }
        public double totalper { get; set; }
        public double offprice { get; set; }
        public double tarprice { get; set; }
        public double cnfrmprice { get; set; }

        public string resdesc { get; set; }
        public string resdesc1 { get; set; }
        public string spcfdesc { get; set; }
        public string spcfdesc1 { get; set; }
        public string spcfdesc2 { get; set; }
        public string colordesc { get; set; }
        public double qty { get; set; }
        public double wstPc { get; set; }
        public string resunit { get; set; }
        public string spcfcode { get; set; }
        public string convrate { get; set; }
        public double cfrate { get; set; }
        public DateTime csumpdat { get; set; }
        public string inqno { get; set; }
        public string pgaprvbyid { get; set; }
        public string curcod { get; set; }
        public string compcode { get; set; }
        public string compdesc { get; set; }
        public string compgrp { get; set; }
        public string cmpgrpdsc { get; set; }
        public string notes { get; set; }
        public double westpc { get; set; }


    }

    public class EclassConsumptionSamSize
    {
        public string comcod { get; set; }
        public string typedesc { get; set; }
        public string s1 { get; set; }
        public string s2 { get; set; }
        public string s3 { get; set; }
        public string s4 { get; set; }
        public string s5 { get; set; }
        public string s6 { get; set; }
        public string s7 { get; set; }
        public string s8 { get; set; }
        public string s9 { get; set; }
        public string s10 { get; set; }

    }
    //a.comcod, a.rescode, a.resdesc, a.resunit, inqno=isnull(b.inqno,'00000000000000'), rate=isnull(b.rate,0.00), amt=isnull(c.stdamt,0.00)

    [Serializable]

    public class EclassCommonCost
    {
        public string comcod { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string inqno { get; set; }
        public double rate { get; set; }
        public double amt { get; set; }
        public double percnt { get; set; }
    }
    [Serializable]

    public class EclassCommonCostSam
    {
        public string comcod { get; set; }
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public double stdamt { get; set; }
        public double percnt { get; set; }
        public EclassCommonCostSam()
        {

        }
    }
    [Serializable]

    public class EclassMerForceEdit
    {
        public string comcod { get; set; }
        public string mlccod { get; set; }
        public string refno { get; set; }
        public string styleid { get; set; }
        public string colorid { get; set; }
        public string procode { get; set; }
        public string rsircode { get; set; }
        public string osircode { get; set; }
        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public string spcfcode { get; set; }
        public string spcfdesc { get; set; }
        public string ospcfcod { get; set; }
        public string compcode { get; set; }
        public string ocompcode { get; set; }
        public string compdesc { get; set; }
        public double conqty { get; set; }
        public double westpc { get; set; }
        public double rstdqty { get; set; }
        public double stdamt { get; set; }
        public string mattype { get; set; }
        public double rate { get; set; }
        public string fgsize { get; set; }
        public EclassMerForceEdit()
        {

        }
    }
    [Serializable]

    public class EclassCurinfo
    {
        public string comcod { get; set; }
        public string sampleid { get; set; }
        public string samptype { get; set; }
        public double curcod { get; set; }
        public string codedesc { get; set; }
        public string curdesc { get; set; }
        public double conrate { get; set; }
        public double tlamt { get; set; }
        public double bdt { get; set; }
        public string curcoda { get; set; }
        public string curdesca { get; set; }
        public string curcodb { get; set; }
        public string curdescb { get; set; }
        public double exratea { get; set; }
        public double exrateb { get; set; }
        public EclassCurinfo()
        {

        }
    }

    [Serializable]
    public class EclassPartInOrder
    {
        public string partcode { get; set; }
        public string partdesc { get; set; }
        public string rsircode { get; set; }
        public string rsirdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string rsircode1 { get; set; }
        public string rsirdesc1 { get; set; }
        public EclassPartInOrder() { }
        public EclassPartInOrder(string partcode, string partdesc, string rsircode, string rsirdesc, string spcfcod, string spcfdesc, string rsirdesc1)
        {
            this.partcode = partcode;
            this.partdesc = partdesc;
            this.rsircode = rsircode;
            this.rsirdesc = rsirdesc;
            this.spcfcod = spcfcod;
            this.spcfdesc = spcfdesc;
            this.rsirdesc1 = rsirdesc1;

        }
    }

    [Serializable]
    //   a.comcod, a.inqno, a.prodcode, a.colorid, a.sizeid, a.procode, prodesc=isnull(b.sirdesc,''), rescode=a.rsircode, resdesc=isnull(c.sirdesc,''), a.spcfcode, spcfdesc=isnull(d.spcfdesc,''), resunit=isnull(c.sirunit,''),
    //a.conqty, a.westpc, qty=a.rstdqty, amt=a.stdamt, rate=iif(a.rstdqty= 0.00, 0.00, a.stdamt/a.rstdqty), compcode='', compname='', percnt=0.00, convrate=0.00
    public class CommonMterailsCal
    {

        public string comcod { get; set; }
        public string inqno { get; set; }
        public string prodcode { get; set; }
        public string colorid { get; set; }
        public string sizeid { get; set; }
        public string procode { get; set; }
        public string prodesc { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public string spcfcode { get; set; }
        public string spcfdesc { get; set; }
        public string resunit { get; set; }
        public double conqty { get; set; }
        public double westpc { get; set; }
        public double qty { get; set; }
        public double amt { get; set; }
        public double rate { get; set; }
        public string compcode { get; set; }
        public string compname { get; set; }
        public double percnt { get; set; }
        public double convrate { get; set; }

    }


    [Serializable]
    public class OrderDetails
    {
        public string comcod { get; set; }
        public string buyerid { get; set; }
        public string buyername { get; set; }
        public string inqno { get; set; }
        public string category { get; set; }
        public string catedesc { get; set; }
        public string styleid { get; set; }
        public string styledesc { get; set; }
        public string artno { get; set; }
        public string ordrno { get; set; }
        public DateTime ordrcvdat { get; set; }
        public DateTime shipmntdat { get; set; }
        public string color { get; set; }
        public string sizernge { get; set; }
        public string images { get; set; }
        public double ordqty { get; set; }
        public double price { get; set; }
        public double cnfrmprice { get; set; }
        public string mlccod { get; set; }
        public string sizeselect { get; set; }
        public string curcod { get; set; }
        public double exrate { get; set; }
        public string currency { get; set; }
        public string autoartcle { get; set; }
        public string mlcdesc { get; set; }
        public string ordcatdesc { get; set; }
        public string bomdate { get; set; }
        public string colorid { get; set; }
        public string colordesc { get; set; }
        public double proplanqty { get; set; }
        public string bomid { get; set; }
        public string dayid { get; set; }
        public double balplnqty { get; set; }

        public string etd { get; set; }
        public string leadtime { get; set; }

    }

    //  a.comcod, a.styleid, a.colorid, a.styledesc, a.colordesc, a.styleunit, description=isnull(c.catedesc,'')+'-'+isnull(c.artno,''),	
    //F7201001
    [Serializable]

    public class GetOrderWithCat
    {

        public string slnum { get; set; }
        public string comcod { get; set; }
        public string dayid { get; set; }
        public string styleid { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string colorid { get; set; }
        public string styledesc { get; set; }
        public string colordesc { get; set; }
        public string styleunit { get; set; }
        public string description { get; set; }
        public string orderno { get; set; }
        public string custordno { get; set; }
        public string custrefno { get; set; }
        public string hscode { get; set; }
        public string formadesc { get; set; }
        public string typeoflebel { get; set; }
        public string packid { get; set; }
        public string packdesc { get; set; }
        public string crtnno { get; set; }
        public string barcodrefno { get; set; }
        public double boxlength { get; set; }
        public double boxwidth { get; set; }
        public double boxheight { get; set; }
        public double cbm { get; set; }
        public double cartoon { get; set; }
        public double s1 { get; set; }
        public double s2 { get; set; }
        public double s3 { get; set; }
        public double s4 { get; set; }
        public double s5 { get; set; }
        public double s6 { get; set; }
        public double s7 { get; set; }
        public double s8 { get; set; }
        public double s9 { get; set; }
        public double s10 { get; set; }
        public double s11 { get; set; }
        public double s12 { get; set; }
        public double s13 { get; set; }
        public double s14 { get; set; }
        public double s15 { get; set; }
        public double s16 { get; set; }
        public double s17 { get; set; }
        public double s18 { get; set; }
        public double s19 { get; set; }
        public double s20 { get; set; }
        public double s21 { get; set; }
        public double s22 { get; set; }
        public double s23 { get; set; }
        public double s24 { get; set; }
        public double s25 { get; set; }
        public double s26 { get; set; }
        public double s27 { get; set; }
        public double s28 { get; set; }
        public double s29 { get; set; }
        public double s30 { get; set; }
        public double s31 { get; set; }
        public double s32 { get; set; }
        public double s33 { get; set; }
        public double s34 { get; set; }
        public double s35 { get; set; }
        public double s36 { get; set; }
        public double s37 { get; set; }
        public double s38 { get; set; }
        public double s39 { get; set; }
        public double s40 { get; set; }
        public double p1 { get; set; }
        public double p2 { get; set; }
        public double p3 { get; set; }
        public double p4 { get; set; }
        public double p5 { get; set; }
        public double p6 { get; set; }
        public double p7 { get; set; }
        public double p8 { get; set; }
        public double p9 { get; set; }
        public double p10 { get; set; }
        public double p11 { get; set; }
        public double p12 { get; set; }
        public double p13 { get; set; }
        public double p14 { get; set; }
        public double p15 { get; set; }
        public double p16 { get; set; }
        public double p17 { get; set; }
        public double p18 { get; set; }
        public double p19 { get; set; }
        public double p20 { get; set; }
        public double p21 { get; set; }
        public double p22 { get; set; }
        public double p23 { get; set; }
        public double p24 { get; set; }
        public double p25 { get; set; }
        public double p26 { get; set; }
        public double p27 { get; set; }
        public double p28 { get; set; }
        public double p29 { get; set; }
        public double p30 { get; set; }
        public double p31 { get; set; }
        public double p32 { get; set; }
        public double p33 { get; set; }
        public double p34 { get; set; }
        public double p35 { get; set; }
        public double p36 { get; set; }
        public double p37 { get; set; }
        public double p38 { get; set; }
        public double p39 { get; set; }
        public double p40 { get; set; }
        public double totalqty { get; set; }
        public double colqty { get; set; }
        public double psum { get; set; }
        public double pairspercrtn { get; set; }
        public double ttlcrtns { get; set; }
        public double ttlpairs { get; set; }
        public double netwgtpercrtn { get; set; }
        public double ttlnetwgt { get; set; }
        public double grswgtpercrtn { get; set; }
        public double ttlgrswgt { get; set; }
        public DateTime exfactorydate { get; set; }
        public GetOrderWithCat()
        {

        }

        public GetOrderWithCat(string comcod, string mlccod, string dayid, string styleid, string colorid,
            string styledesc, string colordesc, string styleunit, string description,
            string custordno, string custrefno, string packid, double cartoon, double s1, double s2, double s3,
            double s4, double s5, double s6, double s7, double s8, double s9, double s10,
            double s11, double s12, double s13, double s14, double s15, double s16, double s17, double s18, double s19,
            double s20, double s21, double s22, double s23, double s24, double s25, double s26, double s27, double s28, double s29, double s30, double s31, double s32,
            double s33, double s34, double s35, double s36, double s37, double s38,
            double s39, double s40, double p1, double p2, double p3, double p4, double p5, double p6, double p7, double p8, double p9, double p10, double p11, double p12, double p13, double p14,
            double p15, double p16, double p17, double p18, double p19, double p20, double p21, double p22,
            double p23, double p24, double p25, double p26, double p27, double p28, double p29, double p30, double p31, double p32, double p33, double p34, double p35, double p36, double p37,
            double p38, double p39, double p40, double totalqty, double colqty, 
            double psum, string slnum
        )
        {
            this.mlccod = mlccod;
            this.dayid = dayid;
            this.comcod = comcod;
            this.styleid = styleid;
            this.colorid = colorid;
            this.styledesc = styledesc;
            this.colordesc = colordesc;
            this.styleunit = styleunit;
            this.description = description;
            this.custordno = custordno;
            this.custrefno = custrefno;
            this.packid = packid;
            this.cartoon = cartoon;
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.s5 = s5;
            this.s6 = s6;
            this.s7 = s7;
            this.s8 = s8;
            this.s9 = s9;
            this.s10 = s10;
            this.s11 = s11;
            this.s12 = s12;
            this.s13 = s13;
            this.s14 = s14;
            this.s15 = s15;
            this.s16 = s16;
            this.s17 = s17;
            this.s18 = s18;
            this.s19 = s19;
            this.s19 = s19;
            this.s20 = s20;
            this.s21 = s21;
            this.s22 = s22;
            this.s23 = s23;
            this.s24 = s24;
            this.s25 = s25;
            this.s26 = s26;
            this.s27 = s27;
            this.s28 = s28;
            this.s29 = s29;
            this.s30 = s30;
            this.s31 = s31;
            this.s32 = s32;
            this.s33 = s33;
            this.s34 = s34;
            this.s35 = s35;
            this.s36 = s36;
            this.s37 = s37;
            this.s38 = s38;
            this.s39 = s39;
            this.s40 = s40;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
            this.p5 = p5;
            this.p6 = p6;
            this.p7 = p7;
            this.p8 = p8;
            this.p9 = p9;
            this.p10 = p10;
            this.p11 = p11;
            this.p12 = p12;
            this.p13 = p13;
            this.p14 = p14;
            this.p15 = p15;
            this.p16 = p16;
            this.p17 = p17;
            this.p18 = p18;
            this.p19 = p19;
            this.p20 = p20;
            this.p21 = p21;
            this.p22 = p22;
            this.p23 = p23;
            this.p24 = p24;
            this.p25 = p25;
            this.p26 = p26;
            this.p27 = p27;
            this.p28 = p28;
            this.p29 = p29;
            this.p30 = p30;
            this.p31 = p31;
            this.p32 = p32;
            this.p33 = p33;
            this.p34 = p34;
            this.p35 = p35;
            this.p36 = p36;
            this.p37 = p37;
            this.p38 = p38;
            this.p39 = p39;
            this.p40 = p40;
            this.totalqty = totalqty;
            this.colqty = colqty;
            this.psum = psum;
            this.slnum = slnum;
        }
    }

    [Serializable]
    public class MergeSizeClass
    {
        public List<SPEENTITY.C_01_Mer.GetOrderWithCat> packsize { get; set; }
        public List<SPEENTITY.C_01_Mer.GetOrderWithCat> colrsize { get; set; }

        public MergeSizeClass() { }
        public MergeSizeClass(List<SPEENTITY.C_01_Mer.GetOrderWithCat> packsize, List<SPEENTITY.C_01_Mer.GetOrderWithCat> colrsize)
        {
            this.packsize = packsize;
            this.colrsize = colrsize;
        }

    }

    [Serializable]

    public class GetOrderWithCatLot
    {
        public string buyerdesc { get; set; }
        public string lotno { get; set; }
        public string slnum { get; set; }
        public string comcod { get; set; }
        public string styleid { get; set; }
        public string colorid { get; set; }
        public string styledesc { get; set; }
        public string colordesc { get; set; }
        public string styleunit { get; set; }
        public string description { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public double s1 { get; set; }
        public double s2 { get; set; }
        public double s3 { get; set; }
        public double s4 { get; set; }
        public double s5 { get; set; }
        public double s6 { get; set; }
        public double s7 { get; set; }
        public double s8 { get; set; }
        public double s9 { get; set; }
        public double s10 { get; set; }
        public double s11 { get; set; }
        public double s12 { get; set; }
        public double s13 { get; set; }
        public double s14 { get; set; }
        public double s15 { get; set; }
        public double s16 { get; set; }
        public double s17 { get; set; }
        public double s18 { get; set; }
        public double s19 { get; set; }
        public double s20 { get; set; }
        public double s21 { get; set; }
        public double s22 { get; set; }
        public double s23 { get; set; }
        public double s24 { get; set; }
        public double s25 { get; set; }
        public double s26 { get; set; }
        public double s27 { get; set; }
        public double s28 { get; set; }
        public double s29 { get; set; }
        public double s30 { get; set; }
        public double s31 { get; set; }
        public double s32 { get; set; }
        public double s33 { get; set; }
        public double s34 { get; set; }
        public double s35 { get; set; }
        public double s36 { get; set; }
        public double s37 { get; set; }
        public double s38 { get; set; }
        public double s39 { get; set; }
        public double s40 { get; set; }
        public double totalqty { get; set; }
        public double colqty { get; set; }


        public GetOrderWithCatLot()
        {

        }
        public GetOrderWithCatLot(string comcod, string styleid, string colorid,
         string styledesc, string colordesc, string styleunit, string description
        , double s1, double s2, double s3,
         double s4, double s5, double s6, double s7, double s8, double s9, double s10,
         double s11, double s12, double s13, double s14, double s15, double s16, double s17, double s18, double s19,
         double s20, double s21, double s22, double s23, double s24, double s25, double s26, double s27, double s28, double s29, double s30, double s31, double s32,
         double s33, double s34, double s35, double s36, double s37, double s38,
         double s39, double s40, double totalqty, double colqty, double psum, string slnum)
        {
            this.comcod = comcod;
            this.styleid = styleid;
            this.colorid = colorid;
            this.styledesc = styledesc;
            this.colordesc = colordesc;
            this.styleunit = styleunit;
            this.description = description;

            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.s5 = s5;
            this.s6 = s6;
            this.s7 = s7;
            this.s8 = s8;
            this.s9 = s9;
            this.s10 = s10;
            this.s11 = s11;
            this.s12 = s12;
            this.s13 = s13;
            this.s14 = s14;
            this.s15 = s15;
            this.s16 = s16;
            this.s17 = s17;
            this.s18 = s18;
            this.s19 = s19;
            this.s19 = s19;
            this.s20 = s20;
            this.s21 = s21;
            this.s22 = s22;
            this.s23 = s23;
            this.s24 = s24;
            this.s25 = s25;
            this.s26 = s26;
            this.s27 = s27;
            this.s28 = s28;
            this.s29 = s29;
            this.s30 = s30;
            this.s31 = s31;
            this.s32 = s32;
            this.s33 = s33;
            this.s34 = s34;
            this.s35 = s35;
            this.s36 = s36;
            this.s37 = s37;
            this.s38 = s38;
            this.s39 = s39;
            this.s40 = s40;
            this.totalqty = totalqty;
            this.colqty = colqty;
            this.slnum = slnum;
        }
    }


    [Serializable]
    public class OrderSummary
    {
        public string mlccod1 { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string unit { get; set; }
        public double qty { get; set; }
        public double fcamt { get; set; }
        public double rate { get; set; }
        public double tamt { get; set; }
        public double peramt { get; set; }

        public string colst { get; set; }
        public OrderSummary()
        {

        }
    }

    [Serializable]
    public class EclassMonthlyMerchandStatus
    {
        public string comcod { get; set; }
        public string mondays { get; set; }
        public double smple { get; set; }
        public double orderqty { get; set; }
        public double reordrqty { get; set; }
    }
    
    [Serializable]
    public class RptPfiList
    {
        public string comcod { get; set; }
        public string pfino { get; set; }
        public string pfino2 { get; set; }
        public string articleno { get; set; }
        public string articledesc { get; set; }
        public DateTime invdate { get; set; }
        public string invno { get; set; }
        public double ordrqty { get; set; }
        public string posteddat { get; set; }
        public string postedbyid { get; set; }
        public string postedbyname { get; set; }
        public string postedbydesig { get; set; }
    }


}
