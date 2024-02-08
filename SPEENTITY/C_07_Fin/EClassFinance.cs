using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEENTITY.C_07_Fin
{
   
    public class EClassPayment 
    {
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string blccod { get; set; }
        public string blcdesc { get; set; }
        public string suplrid { get; set; }
        public string supldesc { get; set; }
        public DateTime suppldate { get; set; }
        public DateTime setdate { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        //public double amt7 { get; set; }
        //public double amt8 { get; set; }
        //public double amt9 { get; set; }
        //public double amt10 { get; set; }
        //public double amt11 { get; set; }
        //public double amt12 { get; set; }
        public double toamt { get; set; }
        public EClassPayment() { }
       
        public EClassPayment(string mlccod, string mlcdesc, string blccod, string blcdesc, string suplrid, string supldesc, DateTime suppldate, DateTime setdate, 
            double amt1, double amt2, double amt3, double amt4, double amt5, double amt6, double toamt) 
        {   
            this.mlccod = mlccod;
            this.mlcdesc = mlcdesc;
            this.blccod = blccod;
            this.blcdesc = blcdesc;
            this.suplrid = suplrid;
            this.supldesc = supldesc;
            this.suppldate = suppldate;
            this.setdate = setdate;
            this.amt1 = amt1;
            this.amt2 = amt2;
            this.amt3 = amt3;
            this.amt4 = amt4;
            this.amt5 = amt5;
            this.amt6 = amt6;
            //this.amt7 = amt7;
            //this.amt8 = amt8;
            //this.amt9 = amt9;
            //this.amt10 = amt10;
            //this.amt11 = amt11;
            //this.amt12 = amt12;
            this.toamt = toamt;
        
        }
    
    }

    public class EClassCollection
    {
        public string mlccod1 { get; set; }
        public string mlcdesc1 { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string invno { get; set; }
        public DateTime shipmdat { get; set; }
        public DateTime settldat { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        public double toamt { get; set; }
        public EClassCollection() { }
        public EClassCollection(string mlccod1, string mlcdesc1, string mlccod, string mlcdesc, string invno, DateTime shipmdat, DateTime settldat,
            double amt1, double amt2, double amt3, double amt4, double amt5, double amt6, double toamt)
        {
            this.mlccod1 = mlccod1;
            this.mlcdesc1 = mlcdesc1;
            this.mlccod = mlccod;
            this.mlcdesc = mlcdesc;
            this.invno = invno;
            this.shipmdat = shipmdat;
            this.settldat = settldat;
            this.amt1 = amt1;
            this.amt2 = amt2;
            this.amt3 = amt3;
            this.amt4 = amt4;
            this.amt5 = amt5;
            this.amt6 = amt6;
            this.toamt = toamt;

        }

    }

    public class EClassCollVsPayment
    {
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public string code3 { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string desc3 { get; set; }
        public DateTime date1 { get; set; }
        public DateTime settldat { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        public double toamt { get; set; }
        public EClassCollVsPayment() { }
        public EClassCollVsPayment(string grp, string grpdesc, string code1, string code2, string code3, string desc1, string desc2, string desc3, DateTime date1, DateTime settldat,
            double amt1, double amt2, double amt3, double amt4, double amt5, double amt6, double toamt)
        {
            this.grp = grp;
            this.grpdesc = grpdesc;
            this.code1 = code1;
            this.code2 = code2;
            this.code3 = code3;
            this.desc1 = desc1;
            this.desc2 = desc2;
            this.desc3 = desc3;
            this.date1 = date1;
            this.settldat = settldat;
            this.amt1 = amt1;
            this.amt2 = amt2;
            this.amt3 = amt3;
            this.amt4 = amt4;
            this.amt5 = amt5;
            this.amt6 = amt6;
            this.toamt = toamt;

        }

    }
    [Serializable]
    public class MaterialWiseStock
    {


        public string gp { get; set; }
        public string grp { get; set; }
        public string isuno { get; set; }
        public string isuno1 { get; set; }
        public string exdate { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string subcode { get; set; }
        public string subdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string unit { get; set; }
        public double inqty { get; set; }
        public double outqty { get; set; }
        public double stock { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public double rate { get; set; }


    }
}
