﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_19_Exp
{
    public class Sales_BO
    {
        [Serializable]
        public class productInfo01
        {
            public string comcod { get; set; }
            public string getpno { get; set; }
            public string getpno1 { get; set; }
            public string getpdat { get; set; }
            public string getpref { get; set; }
            public string preqno { get; set; }
            public string preqno1 { get; set; }
            public string rsircode { get; set; }
            public double getpqty { get; set; }
            public double getpamt { get; set; }
            public double rate { get; set; }
            public double mtrfqty { get; set; }
            public double prret { get; set; }
            public string tfpactcode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string textfild { get; set; }
            public string valuefiled { get; set; }
            public string tfpactdesc { get; set; }
            public string ttpactdesc { get; set; }
            public double Rcvqty { get; set; }

        }

        [Serializable]
        public class SalesCenter
        {
            public string comcod { set; get; }
            public string actcode { set; get; }
            public string actdesc { set; get; }
            public SalesCenter() { }
            public SalesCenter(string comcod, string actcode, string actdesc)
            {
                this.comcod = comcod;
                this.actcode = actcode;
                this.actdesc = actdesc;
            }
        }///SalesInvNew
        [Serializable]
        public class DebtorList
        {
            public string actcode { set; get; }
            public string custid { set; get; }
            public string custname { set; get; }
            public string curcode { set; get; }
            public double limit { set; get; }
            public double duesamt { set; get; }
            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public double ballimit { set; get; }
            public double proamt { set; get; }
            public string custname2 { set; get; }
            public string custaddr { set; get; }
            public string regcode { set; get; }
            public DebtorList() { }
            public DebtorList(string actcode, string custid, string custname, string curcode, double limit, double duesamt, string ssircode, string ssirdesc, double ballimit,
                double proamt, string custname2, string custaddr, string regcode)
            {
                this.actcode = actcode;
                this.custid = custid;
                this.custname = custname;
                this.curcode = curcode;
                this.limit = limit;
                this.duesamt = duesamt;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.ballimit = ballimit;
                this.proamt = proamt;
                this.custname2 = custname2;
                this.custaddr = custaddr;
                this.regcode = regcode;


            }
        }
        [Serializable]
        public class TaxInfo
        {
            public string taxcode { set; get; }
            public string taxdesc { set; get; }
            public double parcnt { set; get; }
            public string method { set; get; }
            public TaxInfo()
            { }
            public TaxInfo(string taxcode, string taxdesc, double parcnt, string method)
            {

                this.taxcode = taxcode;
                this.taxdesc = taxdesc;
                this.parcnt = parcnt;
                this.method = method;
            }
        }

        [Serializable]
        public class TeamList
        {
            public string actcode { set; get; }
            public string usircode { set; get; }
            public string teamcode { set; get; }
            public string teamdesc { set; get; }
            public string custname { set; get; }
            public TeamList() { }
            public TeamList(string actcode, string usircode, string teamcode, string teamdesc, string custname)
            {

                this.actcode = actcode;
                this.usircode = usircode;
                this.teamcode = teamcode;
                this.teamdesc = teamdesc;
                this.custname = custname;
            }
        }

        [Serializable]
        public class ProductList
        {   public string comcod { get; set; }
            public string actcode { set; get; }
            public string prcod { set; get; }
            public string prdesc { set; get; }
            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public double avlablqty { set; get; }
            public double tmprice { set; get; }
            public double price { set; get; }
            public bool wastatus { set; get; }
            public string unit { set; get; }
            public string prodscode { set; get; }
            public string taxcode { set; get; }
            public string ordqty { set; get; }
            public string promqty { set; get; }
            public double amount { get; set; }
            public string prtdesc { set; get; }
            public double upordqty { set; get; }
            public double balstkqyt { set; get; }
            public double disinper { get; set; }

            public ProductList() { }
            public ProductList(string actcode, string prcod, string prdesc, string batchcode, string batchdesc, double avlablqty, double tmprice, double price,
                bool wastatus, string unit, string prodscode, string taxcode, string ordqty, string promqty, double amount, string prtdesc, double upordqty, double balstkqyt, double disinper)
            {
                this.actcode = actcode;
                this.prcod = prcod;
                this.prdesc = prdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.avlablqty = avlablqty;
                this.tmprice = tmprice;
                this.price = price;
                this.wastatus = wastatus;
                this.unit = unit;
                this.prodscode = prodscode;
                this.taxcode = taxcode;
                this.ordqty = ordqty;
                this.promqty = promqty;
                this.amount = amount;
                this.prtdesc = prtdesc;
                this.upordqty = upordqty;
                this.balstkqyt = balstkqyt;
                this.disinper = disinper;
            }
        }

        [Serializable]
        public class ProProductList
        {
            public string prodcode { set; get; }
            public double ordqty { set; get; }
            public double promqty { set; get; }
            public double prrate { get; set; }

            public ProProductList() { }
            public ProProductList(string prodcode, double ordqty, double promqty, double prrate)
            {
                this.prodcode = prodcode;
                this.ordqty = ordqty;
                this.promqty = promqty;
                this.prrate = prrate;
            }
        }
        [Serializable]

        public class prowithcaragory
        {
            public string actcode { set; get; }
            public string prcod { set; get; }
            public string prdesc { set; get; }
            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public double avlablqty { set; get; }
            public double tmprice { set; get; }
            public double price { set; get; }
            public bool wastatus { set; get; }
            public string unit { set; get; }
            public string prodscode { set; get; }
            public string taxcode { set; get; }
            public string ordqty { set; get; }
            public string promqty { set; get; }
            public string procat { get; set; }
            public string procatdesc { get; set; }
            public string prosubcat { get; set; }
            public string prosubcatdesc { get; set; }
            public double amount { get; set; }

            public prowithcaragory(string actcode, string prcod, string prdesc, string batchcode, string batchdesc, double avlablqty, double tmprice, double price,
                bool wastatus, string unit, string prodscode, string taxcode, string ordqty, string promqty, string procat, string procatdesc, string prosubcat, string prosubcatdesc, double amount)
            {
                this.actcode = actcode;
                this.prcod = prcod;
                this.prdesc = prdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.avlablqty = avlablqty;
                this.tmprice = tmprice;
                this.price = price;
                this.wastatus = wastatus;
                this.unit = unit;
                this.prodscode = prodscode;
                this.taxcode = taxcode;
                this.ordqty = ordqty;
                this.promqty = promqty;
                this.procat = procat;
                this.procatdesc = procatdesc;
                this.prosubcat = prosubcat;
                this.prosubcatdesc = prosubcatdesc;
                this.amount = amount;
            }
            public prowithcaragory() { }
        }

        [Serializable]
        public class ProductLot
        {
            public string actcode { set; get; }
            public string prcod { set; get; }
            public string prdesc { set; get; }
            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public double avlablqty { set; get; }
            public double tmprice { set; get; }
            public double price { set; get; }
            public bool wastatus { set; get; }
            public string unit { set; get; }
            public string prodscode { set; get; }
            public ProductLot() { }
            public ProductLot(string actcode, string prcod, string prdesc, string batchcode, string batchdesc, double avlablqty, double tmprice, double price, bool wastatus, string unit, string prodscode)
            {

                this.actcode = actcode;
                this.prcod = prcod;
                this.prdesc = prdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.avlablqty = avlablqty;
                this.tmprice = tmprice;
                this.price = price;
                this.wastatus = wastatus;
                this.unit = unit;
                this.prodscode = prodscode;
            }
        }

        [Serializable]
        public class ConvInf
        {
            public string fcode { set; get; }
            public string tcode { set; get; }
            public string fcodedesc { set; get; }
            public string tcodedesc { set; get; }
            public double conrate { set; get; }
            public ConvInf() { }
            public ConvInf(string fcode, string tcode, string fcodedesc, string tcodedesc, double conrate)
            {

                this.fcode = fcode;
                this.tcode = tcode;
                this.fcodedesc = fcodedesc;
                this.tcodedesc = tcodedesc;
                this.conrate = conrate;
            }
        }
        [Serializable]
        public class Currencyinf
        {
            public string curcode { set; get; }
            public string curdesc { set; get; }
            public bool curstatus { set; get; }
            public Currencyinf() { }
            public Currencyinf(string curcode, string curdesc, bool curstatus)
            {

                this.curcode = curcode;
                this.curdesc = curdesc;
                this.curstatus = curstatus;
            }
        }
        [Serializable]
        public class LastInvNo
        {
            public string maxsinvno { set; get; }
            public string maxsinvno1 { set; get; }
            public LastInvNo() { }
            public LastInvNo(string maxsinvno, string maxsinvno1)
            {

                this.maxsinvno = maxsinvno;
                this.maxsinvno1 = maxsinvno1;
            }
        }

        [Serializable]
        public class ShowInvItem
        {
            public string actcode { set; get; }
            public string usircode { set; get; }
            public string subcode { set; get; }
            public string batchcode { set; get; }
            public string sdelno { set; get; }

            public string taxcode { set; get; }
            public string actdesc { set; get; }
            public string subdesc { set; get; }
            public string batchdesc { set; get; }
            public string unit { set; get; }

            public double trnqty { set; get; }
            public double avqty { set; get; }
            public double frqty { set; get; }
            public double trnrate { set; get; }
            public double unittax { set; get; }
            public double amount { set; get; }
            public double taxamt { set; get; }
            public double percnt { set; get; }
            public double discount { set; get; }
            public double tamount { set; get; }

            public string curcode { set; get; }
            public double conrate { set; get; }
            public string method { set; get; }
            public Int16 seq { set; get; }
            public Int16 chk { set; get; }
            public string pslno { set; get; }
            public string prodscode { set; get; }

            public Int16 wastatus { set; get; }
            public ShowInvItem() { }
            public ShowInvItem(string actcode, string usircode, string subcode, string batchcode, string sdelno, string taxcode, string actdesc, string subdesc, string batchdesc,
                string unit, double trnqty, double avqty, double frqty, double trnrate, double unittax, double amount, double taxamt, double percnt,
                double discount, double tamount, string curcode, double conrate, string method, Int16 seq, Int16 chk, string pslno, Int16 wastatus, string prodscode)
            {
                this.actcode = actcode;
                this.usircode = usircode;
                this.subcode = subcode;
                this.batchcode = batchcode;
                this.sdelno = sdelno;
                this.taxcode = taxcode;
                this.actdesc = actdesc;
                this.subdesc = subdesc;
                this.batchdesc = batchdesc;
                this.unit = unit;

                this.trnqty = trnqty;
                this.avqty = avqty;
                this.frqty = frqty;
                this.trnrate = trnrate;
                this.unittax = unittax;
                this.amount = amount;
                this.taxamt = taxamt;
                this.percnt = percnt;
                this.discount = discount;
                this.tamount = tamount;

                this.curcode = curcode;
                this.conrate = conrate;
                this.method = method;
                this.seq = seq;
                this.chk = chk;
                this.pslno = pslno;
                this.wastatus = wastatus;
                this.prodscode = prodscode;
            }


        }

        [Serializable]
        public class InvListA
        {
            public string actcode { set; get; }
            public string prcod { set; get; }
            public string batchcode { set; get; }
            public string sdelno { set; get; }

            public double itmqty { set; get; }
            public double itmamt { set; get; }
            public double itmdis { set; get; }
            public double vat { set; get; }

            //comcod, centrid, memono, prcod, itmqty, itmamt, itmdis, vat, grweight, netweight, crtnqty, crtnslno, batchcode, sdelno, wardate
        }
        [Serializable]
        public class InvListB
        {
            public string actcode { set; get; }
            public string custid { set; get; }
            public string teamcode { set; get; }
            public string orderno { set; get; }
            public string invdis { set; get; }
            public string refno { set; get; }
            public string narration { set; get; }
            public string postedbyid { set; get; }
            public string postseson { set; get; }
            public string postrmid { set; get; }
            public string posteddat { set; get; }
            public Int16 freepro { set; get; }
            //comcod, centrid, memono, memodat, refno, narration, postedbyid, postseson, postrmid, orderno, paymnttrm, invdis, custid, posteddat, teamcode, freepro

        }

        [Serializable]
        public class SalesExp_BO
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double salupamt { get; set; }
            public double salcuamt { get; set; }
            public double expupamt { get; set; }
            public double excuamt { get; set; }
            public double salpercent { get; set; }
            public double exppercent { get; set; }
            public SalesExp_BO(string actcode, string actdesc, double salupamt, double salcuamt, double expupamt, double excuamt, double salpercent, double exppercent)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.salupamt = salupamt;
                this.salcuamt = salcuamt;
                this.expupamt = expupamt;
                this.excuamt = excuamt;
                this.salpercent = salpercent;
                this.exppercent = exppercent;

            }
        }

        [Serializable]
        public class SalesLedger_BO
        {
            public string centrid { get; set; }
            public string custid { get; set; }
            public string custdesc { get; set; }
            public double itmamt { get; set; }
            public double itmdis { get; set; }
            public double invdis { get; set; }
            public double totaldis { get; set; }
            public double vat { get; set; }
            public double totalrecamt { get; set; }
            public double freeamt { get; set; }
            public SalesLedger_BO(string centrid, string custid, string custdesc, double itmamt, double itmdis, double invdis, double totaldis, double vat, double totalrecamt, double freeamt)
            {
                this.centrid = centrid;
                this.custid = custid;
                this.custdesc = custdesc;
                this.itmamt = itmamt;
                this.itmdis = itmdis;
                this.invdis = invdis;
                this.totaldis = totaldis;
                this.vat = vat;
                this.totalrecamt = totalrecamt;
                this.freeamt = freeamt;
            }
        }

        [Serializable]
        public class AllCustList
        {
            public string actcode { set; get; }
            public string custid { set; get; }
            public string custname { set; get; }
            public double limit { set; get; }
            public AllCustList() { }
            public AllCustList(string actcode, string custid, string custname, double limit)
            {
                this.actcode = actcode;
                this.custid = custid;
                this.custname = custname;
                this.limit = limit;
            }
        }
        [Serializable]
        public class EClassGetAllInv
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }

            public string custid { set; get; }
            public string custdesc { set; get; }
            public string memono { set; get; }
            public string memono1 { set; get; }
            public string vounum1 { set; get; }
            public string memodat { set; get; }
            public double itmamt { set; get; }
            public double vat { set; get; }
            public double invdis { set; get; }
            public double vatper { set; get; }
            public double invdisper { set; get; }
            public string payvounum { set; get; }
            public string teamcode { set; get; }
            public string teamdesc { set; get; }
            public string tercode { set; get; }
            public string territory { set; get; }

            public EClassGetAllInv() { }


            public EClassGetAllInv(string centrid, string centrdesc, string custid, string custdesc, string memono, string memono1, string vounum1, string memodat,
                double itmamt, double vat, double invdis, double vatper, double invdisper, string payvounum, string teamcode, string teamdesc, string tercode, 
                string territory)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.memono = memono;
                this.memono1 = memono1;
                this.vounum1 = vounum1;
                this.memodat = memodat;
                this.itmamt = itmamt;
                this.vat = vat;
                this.invdis = invdis;
                this.vatper = vatper;
                this.invdisper = invdisper;
                this.payvounum = payvounum;
                this.teamcode = teamcode;
                this.teamdesc = teamdesc;
                this.tercode = tercode;
                this.territory = territory;
            }
        }



        [Serializable]
        public class Paytype
        {
            public string paytype { set; get; }
            public string paydesc { set; get; }

            public Paytype() { }
            public Paytype(string paytype, string paydesc)
            {

                this.paytype = paytype;
                this.paydesc = paydesc;

            }
        }


        [Serializable]
        public class ClBank
        {
            public string actcode { set; get; }
            public string actdesc { set; get; }

            public ClBank() { }
            public ClBank(string actcode, string actdesc)
            {

                this.actcode = actcode;
                this.actdesc = actdesc;

            }
        }


        [Serializable]
        public class InstllmentCodeDetails
        {

            public string inscode { get; set; }
            public string insdesc { get; set; }

            public InstllmentCodeDetails()
            {

            }
            public InstllmentCodeDetails(string inscode, string insdesc)
            {
                this.inscode = inscode;
                this.insdesc = insdesc;
            }
        }
        [Serializable]
        public class AllCellCenter
        {
            public string actcode { get; set; }

            public string actdesc { get; set; }
        }
        [Serializable]
        public class Dues
        {


            public string comcod { get; set; }
            public string centrid { get; set; }
            public string actdesc { get; set; }
            public string memono { get; set; }
            public string custid { get; set; }
            public string custname { get; set; }
            public double salamt { get; set; }
            public double mramt { get; set; }
            public double recAble { get; set; }
            public double currentamt { get; set; }
            public double overamt { get; set; }
            public double delayamt { get; set; }
            public double netdues { get; set; }
            public double totalDue { get; set; }


        }
        [Serializable]
        public class ProAgaing
        {
            public string comcod { get; set; }
            public string rcvdate { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string rescod { get; set; }
            public string resdesc { get; set; }
            public double rcvqty { get; set; }
            public double delqty { get; set; }
            public double balqty { get; set; }
            public string againg { get; set; }

            public ProAgaing(string comcod, string rcvdate, string actcode, string actdesc, string rescod, string resdesc, double rcvqty, double delqty, double balqty, string againg)
            {
            }
            public ProAgaing()
            {
                this.comcod = comcod;
                this.rcvdate = rcvdate;
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.rescod = rescod;
                this.resdesc = resdesc;
                this.rcvqty = rcvqty;
                this.delqty = delqty;
                this.balqty = balqty;
                this.againg = againg;
            }
        }




        public class AlllcCost : SerCentrCost
        {
            public AlllcCost()
            { }
        }

        [Serializable]
        public class SalColDues
        {
            public string comcod { get; set; }
            public string grp { get; set; }

            public string centrid { get; set; }
            public string teamcode { get; set; }
            public string teamdesc { get; set; }
            public DateTime joindate { get; set; }
            public string desg { get; set; }
            public string idcard { get; set; }
            public string circle { get; set; }
            public string region { get; set; }
            public string area { get; set; }

            public string teritory { get; set; }
            public double opamt { get; set; }

            public double salamt { get; set; }
            public double collamt { get; set; }
            public double retamt { get; set; }
            public double tduesamt { get; set; }
            public double s1 { get; set; }
            public double c1 { get; set; }
            public double r1 { get; set; }
            public double d1 { get; set; }
            public double s2 { get; set; }
            public double c2 { get; set; }
            public double r2 { get; set; }
            public double d2 { get; set; }
            public double cd2 { get; set; }
            public double s3 { get; set; }
            public double c3 { get; set; }
            public double r3 { get; set; }
            public double d3 { get; set; }
            public double cd3 { get; set; }
            public double s4 { get; set; }
            public double c4 { get; set; }
            public double r4 { get; set; }
            public double d4 { get; set; }
            public double cd4 { get; set; }
            public double s5 { get; set; }
            public double c5 { get; set; }
            public double r5 { get; set; }
            public double d5 { get; set; }
            public double cd5 { get; set; }
            public double s6 { get; set; }
            public double c6 { get; set; }
            public double r6 { get; set; }
            public double d6 { get; set; }
            public double cd6 { get; set; }
            public double s7 { get; set; }
            public double c7 { get; set; }
            public double r7 { get; set; }
            public double d7 { get; set; }
            public double cd7 { get; set; }
            public double s8 { get; set; }
            public double c8 { get; set; }
            public double r8 { get; set; }
            public double d8 { get; set; }
            public double cd8 { get; set; }
            public double s9 { get; set; }
            public double c9 { get; set; }
            public double r9 { get; set; }
            public double d9 { get; set; }
            public double cd9 { get; set; }
            public double s10 { get; set; }
            public double c10 { get; set; }
            public double r10 { get; set; }
            public double d10 { get; set; }
            public double cd10 { get; set; }
            public double s11 { get; set; }
            public double c11 { get; set; }
            public double r11 { get; set; }
            public double d11 { get; set; }
            public double cd11 { get; set; }
            public double s12 { get; set; }
            public double c12 { get; set; }
            public double r12 { get; set; }
            public double d12 { get; set; }
            public double cd12 { get; set; }



            public SalColDues(string comcod, string grp, string centrid, string teamcode, string teamdesc, string circle, string region, string area, string teritory, DateTime joindate, string desg, string idcard, double salamt,
                double collamt, double s1, double c1, double r1, double d1, double s2, double c2, double r2, double d2, double cd2, double s3, double c3, double r3, double d3, double cd3,
                double s4, double c4, double r4, double d4, double cd4, double s5, double c5, double r5, double d5, double cd5,
                double s6, double c6, double r6, double d6, double cd6, double s7, double c7, double r7, double d7, double cd7,
                double s8, double c8, double r8, double d8, double cd8, double s9, double c9, double r9, double d9, double cd9, double s10, double c10, double r10, double d10, double cd10, double s11, double c11, double r11, double d11, double cd11, double s12, double c12, double r12, double d12, double cd12)
            {

            }

            public SalColDues()
            {

                this.comcod = comcod;
                this.grp = grp;
                this.centrid = centrid;
                this.teamcode = teamcode;
                this.teamdesc = teamdesc;
                this.circle = circle;
                this.region = region;
                this.area = area;
                
                this.teritory = teritory;
                this.joindate = joindate;
                this.desg = desg;
                this.idcard = idcard;
                this.salamt = salamt;
                this.collamt = collamt;
                this.s1 = s1;
                this.c1 = c1;
                this.r1 = r1;
                this.d1 = d1;
                this.s2 = s2;
                this.c2 = c2;
                this.r2 = r2;
                this.d2 = d2;
                this.cd2 = cd2;

                this.s3 = s3;
                this.c3 = c3;
                this.r3 = r3;
                this.d3 = d3;
                this.cd3 = cd3;
                this.s4 = s4;
                this.c4 = c4;
                this.r4 = r4;
                this.d4 = d4;
                this.cd4 = cd4;
                this.s5 = s5;
                this.c5 = c5;
                this.r5 = r5;
                this.d5 = d5;
                this.cd5 = cd5;
                this.s6 = s6;
                this.c6 = c6;
                this.r6 = r6;
                this.d6 = d6;
                this.cd6 = cd6;
                this.s7 = s7;

                this.c7 = c7;
                this.r7 = r7;

                this.d7 = d7;
                this.cd7 = cd7;
                this.s8 = s8;
                this.c8 = c8;
                this.r8 = r8;
                this.d8 = d8;
                this.cd8 = cd8;
                this.s9 = s9;
                this.c9 = c9;
                this.r9 = r9;
                this.d9 = d9;
                this.cd9 = cd9;
                this.s10 = s10;
                this.c10 = c10;
                this.r10 = r10;
                this.d10 = d10;
                this.cd10 = cd10;
                this.s11 = s11;
                this.c11 = c11;
                this.r11 = r11;
                this.d11 = d11;

                this.cd11 = cd11;

                this.s12 = s12;
                this.c12 = c12;
                this.d12 = c12;
                this.r12 = r12;
                this.cd12 = cd12;
            }







        }


        [Serializable]
        public class SerCentrCost
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double trnam { get; set; }
            public double r1 { get; set; }
            public double r2 { get; set; }
            public double r3 { get; set; }
            public double r4 { get; set; }
            public double r5 { get; set; }
            public double r6 { get; set; }
            public double r7 { get; set; }
            public double r8 { get; set; }
            public double r9 { get; set; }
            public double r10 { get; set; }
            public double r11 { get; set; }
            public double r12 { get; set; }
            public double r13 { get; set; }
            public double r14 { get; set; }
            public double r15 { get; set; }
            public double r16 { get; set; }
            public double r17 { get; set; }
            public double r18 { get; set; }
            public double r19 { get; set; }
            public double r20 { get; set; }
            public double r21 { get; set; }
            public double r22 { get; set; }
            public double r23 { get; set; }
            public double r24 { get; set; }
            public double r25 { get; set; }
            public double r26 { get; set; }
            public double r27 { get; set; }
            public double r28 { get; set; }
            public double r29 { get; set; }
            public double r30 { get; set; }
            public double r31 { get; set; }
            public double r32 { get; set; }
            public double r33 { get; set; }
            public double r34 { get; set; }
            public double r35 { get; set; }




            public SerCentrCost(string comcod, string grp, string actcode, string actdesc, double trnam, double r1, double r2, double r3, double r4, double r5, double r6,
                double r7, double r8, double r9, double r10, double r11, double r12, double r13, double r14, double r15, double r16, double r17, double r18, double r19,
                double r20, double r21, double r22, double r23, double r24, double r25, double r26, double r27, double r28, double r29, double r30, double r31, double r32,
                double r33, double r34, double r35)
            {
            }
            public SerCentrCost()
            {
                this.comcod = comcod;
                this.grp = grp;
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.trnam = trnam;
                this.r1 = r1;
                this.r2 = r2;
                this.r3 = r3;
                this.r4 = r4;
                this.r5 = r5;
                this.r6 = r6;
                this.r7 = r7;
                this.r8 = r8;
                this.r9 = r9;
                this.r10 = r10;
                this.r11 = r11;
                this.r12 = r12;
                this.r13 = r13;
                this.r14 = r14;
                this.r15 = r15;
                this.r16 = r16;
                this.r17 = r17;
                this.r18 = r18;
                this.r19 = r19;
                this.r20 = r20;
                this.r21 = r21;
                this.r22 = r22;
                this.r23 = r23;
                this.r24 = r24;
                this.r25 = r25;
                this.r26 = r26;
                this.r27 = r27;
                this.r28 = r28;
                this.r29 = r29;
                this.r30 = r30;
                this.r31 = r31;
                this.r32 = r32;
                this.r33 = r33;
                this.r34 = r34;
                this.r35 = r35;

            }
        }


        [Serializable]
        public class EClassCompany
        {
            public string comcod { get; set; }
            public string comdesc { get; set; }
            public string mcomcod { get; set; }


            public EClassCompany() { }
            public EClassCompany(string comcod, string comdesc, string mcomcod)
            {

                this.comcod = comcod;
                this.comdesc = comdesc;
                this.mcomcod = mcomcod;

            }
        }
        [Serializable]
        public class EClassNonLiCust
        {
            public string custid { get; set; }
            public string custdesc { get; set; }
            public string custcode { get; set; }
            public string teamcode { get; set; }
            public string teamdesc { get; set; }
            public string supcode { get; set; }
            public string supdesc { get; set; }
            public string supcode1 { get; set; }
            public string supdesc1 { get; set; }
            public string circode { get; set; }
            public string circle { get; set; }
            public string regcode { get; set; }
            public string region { get; set; }
            public string areacode { get; set; }
            public string area { get; set; }
            public string tericode { get; set; }
            public string territory { get; set; }
            public string chnlcode { get; set; }
            public string channel { get; set; }
            public EClassNonLiCust() { }

        }

        [Serializable]
        public class EClassdWiseSales
        {
            public string mtype { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string memodat { get; set; }
            public double delqty { get; set; }
            public double delamt { get; set; }
            public double retqty { get; set; }
            public double retamt { get; set; }
            public string circode { get; set; }
            public string circle { get; set; }
            public string regcode { get; set; }
            public string region { get; set; }
            public string areacode { get; set; }
            public string area { get; set; }
            public string tericode { get; set; }
            public string territory { get; set; }
            public string chnlcode { get; set; }
            public string channel { get; set; }
            public EClassdWiseSales() { }

        }
        [Serializable]
        public class EClassdPrDel
        {
            public string centrid { get; set; }
            public string centrdesc { get; set; }
            public string memono { get; set; }
            public string memono1 { get; set; }
            public string sdelno { get; set; }
            public string sdelno1 { get; set; }
            public string custid { get; set; }
            public string custdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string memodat { get; set; }
            public double delqty { get; set; }
            public double retqty { get; set; }

            public double delamt { get; set; }
            public string circode { get; set; }
            public string circle { get; set; }
            public string regcode { get; set; }
            public string region { get; set; }
            public string areacode { get; set; }
            public string area { get; set; }
            public string tericode { get; set; }
            public string territory { get; set; }
            public string chnlcode { get; set; }
            public string channel { get; set; }
            public EClassdPrDel() { }

        }

       // [Serializable]
        //public class ShowInvItem01
        //{
        //    public string centrid { set; get; }
        //    public string centrdesc { set; get; }
        //    public string procode { set; get; }
        //    public string prodesc { set; get; }
        //    public string batchcode { set; get; }
        //    public string batchdesc { set; get; }
        //    public string memono { set; get; }
        //    public string memono1 { set; get; }
        //    public DateTime memodat { set; get; }
        //    public double rate { set; get; }
        //    public double qty { set; get; }
        //    public double amount { set; get; }
        //    public double discount { set; get; }
        //    public double vat { set; get; }
        //    public string ssircode { set; get; }
        //    public string ssirdesc { set; get; }
        //    public string transdet1 { set; get; }
        //    public string transdet2 { set; get; }
        //    public string transdet3 { set; get; }
        //    public string custid { set; get; }
        //    public string custname { set; get; }
        //    public string unit { set; get; }
        //    public string proslnum { set; get; }
        //    public string codenum { set; get; }
        //    public string colorcode { set; get; }
        //    public double ordqty { set; get; }
        //    public double delqty { set; get; }
        //    public double tqty { set; get; }
        //    public string remarks { set; get; }
        //    public double unitvat { set; get; }
        //    public double dispar { set; get; }
        //    public double vatwithamt { set; get; }
        //    public string size { set; get; }
        //    public string grp { set; get; }
        //    public string smctn { set; get; }
        //    public string bigctn { set; get; }
        //    public string procat { set; get; }
        //    public string procatdesc { set; get; }
        //    public string scode { set; get; }
        //    public string packsize { set; get; }
        //}

        [Serializable]
        public class ShowInvItem01
        {
            public string comcod { set; get; }
            public string centrid { set; get; }
            public string centrdesc { set; get; }
            public string procode { set; get; }
            public string prodesc { set; get; }
            public string memono { set; get; }
            public string size { set; get; }
            public string custid { set; get; }
            public string custname { set; get; }
            public string unit { set; get; }
            public double rate { set; get; }
            public double qty { set; get; }
            public double amount { set; get; }
            public double discount { set; get; }
            public double vat { set; get; }
            public double unitvat { set; get; }
            public string dispar { set; get; }
            public double indispar { set; get; }
            public double vatwithamt { set; get; }
            public double freeqty { set; get; }


          
        }

        [Serializable]
        public class SalMargin // PRJINF             //  COMCOD, userid, GCOD, GUNIT, GDATAT, GDATAD, GDATAN
        {
            public string comcod { get; set; }
            public string userid { get; set; }
            public int orderid { get; set; }
            public double minamt { set; get; }
            public double maxamt { set; get; }
            public double dispar { set; get; }
            public SalMargin()
            {

            }
            public SalMargin(string _comcod = "", string _userid = "", int _orderid = 0, double _minamt = 0.00, double _maxamt = 0.00, double _dispar = 0.00)
            {
                this.comcod = _comcod;
                this.userid = _userid;
                this.orderid = _orderid;
                this.minamt = _minamt;
                this.maxamt = _maxamt;
                this.dispar = _dispar;
            }
        }

        //Sales- Realization Certificate
        [Serializable]
        public class SalesRealCertificate
        {
           
            public string comcod { get; set; }
            public string invno { get; set; }
            public string invrefno { get; set; }
            public string mlccod { get; set; }
            public string buyerid { get; set; }
            public double invexrate { get; set; }
            public double ordramt { get; set; }
            public double ordramtbdt { get; set; }
            public double salefc { get; set; }
            public double rcvfc { get; set; }
            public double marginfc { get; set; }
            public double exmacfc { get; set; }
            public double erqacfc { get; set; }
            public double cmfc { get; set; }
            public double fcbnkcharge { get; set; }
            public double recvexrate { get; set; }
            public double salesamt { get; set; }
            public double marginamt { get; set; }
            public double exmacamt { get; set; }
            public double erqacamt { get; set; }
            public double cmamt { get; set; }
            public double bnkchargeamt { get; set; }
            public string buyerdesc { get; set; }
            public string lcdesc { get; set; }
            public double exgainlossamt { get; set; }
            public double duesfc { get; set; }
            public string invno1 { get; set; }
            public string curdesc { get; set; }
            public string cursymbol { get; set; }
            public SalesRealCertificate()
            {

            }

            public SalesRealCertificate(string comcod, string invno, string invrefno,  string mlccod, string buyerid,  double invexrate,
                double ordramt, double ordramtbdt, double salefc, double rcvfc, double marginfc, double exmacfc, double erqacfc,double cmfc, double fcbnkcharge, double recvexrate,
                double salesamt, double marginamt, double exmacamt, double erqacamt, double cmamt, double bnkchargeamt, string buyerdesc, string lcdesc, double exgainlossamt, double duesfc, string invno1, string curdesc, string cursymbol)
            {
                this.comcod = comcod;
                this.invno = invno;
                this.invrefno = invrefno;
                this.mlccod = mlccod;
                this.buyerid = buyerid;
                this.invexrate = invexrate;
                this.ordramt = ordramt;
                this.ordramtbdt = ordramtbdt;
                this.salefc = salefc;
                this.rcvfc = rcvfc;
                this.marginfc = marginfc;
                this.exmacfc = exmacfc;
                this.erqacfc = erqacfc;
                this.cmfc = cmfc;
                this.fcbnkcharge = fcbnkcharge;
                this.recvexrate = recvexrate;
                this.salesamt = salesamt;
                this.marginamt = marginamt;
                this.exmacamt = exmacamt;
                this.erqacamt = erqacamt;
                this.cmamt = cmamt;
                this.bnkchargeamt = bnkchargeamt;
                this.buyerdesc = buyerdesc;
                this.lcdesc = lcdesc;
                this.exgainlossamt = exgainlossamt;
                this.duesfc = duesfc;
                this.invno1 = invno1;
                this.curdesc = curdesc;
                this.cursymbol = cursymbol;
            }
        }
    }
}
