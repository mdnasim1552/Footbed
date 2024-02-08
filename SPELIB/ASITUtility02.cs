using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;

using System.Security.Cryptography;


namespace SPELIB
{
    public static  class ASITUtility02
    {
        
        public static bool TransactionDateCon(DateTime bdate, DateTime date)
        {
            DateTime Sysdate = System.DateTime.Today;
            bool result = (bdate <date &&  date<=Sysdate) ? true : false;
            return result;

        }

        public static bool TransPostDateCheque(DateTime chqdate, DateTime date) 
        {
            bool result = (date >= chqdate) ? true : false;
            return result;
        }


        public static bool TransReconDate(DateTime recondate, DateTime date)
        {
            bool result = (date <= recondate) ? true : false;
            return result;
        }



        public static bool PurChaseOperation(DateTime date1, DateTime date2)
        {

            bool result = (date1 <=date2) ? true : false;
            return result;

        }

        
   public static  List<string> pasyear(int frmyear, int toyear)


    {

       List<string> passyear = new List<string>();
        while (frmyear <= toyear) 
        {


            passyear.Add(frmyear.ToString());
            frmyear = frmyear + 1;
        }

        return (passyear);
    }


   public static string DecryptValue(string Name) 
   {

      
       string DecryptName = "";
       int a = 5;
       char name;
       for (int i = 0; i < Name.Length; i++)
       {
           name = Convert.ToChar(Name.Substring(i, 1));
           DecryptName =DecryptName+Convert.ToChar(name - a).ToString();
       }

       return DecryptName;
   }

   public static string EncryptValue(string Name)
   {

 
       string EncryptName = "";
       int a = 5;
       char name;
       for (int i = 0; i < Name.Length; i++)
       {
           name = Convert.ToChar(Name.Substring(i, 1));
           EncryptName = EncryptName + Convert.ToChar(name + a).ToString();
       }

       return EncryptName;
   }


   public static string ToBangla(string enamt)
   {
       // 25369
       string banamt = "";
       char[] eng = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
       char[] ban = { '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯' };
       int len = enamt.Length;
       foreach (var x in enamt)
       {
           string v = x.ToString();
           banamt += (v == "." || v == "," || v == "-" ? v : ban[int.Parse(v)].ToString());
       }

       return banamt;
   }

        public static string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }
        public static string GetFullMonthName(string name)
        {
            return name.Replace("January", "জানুয়ারী").Replace("February", "ফেব্রুয়ারী").Replace("March", "মার্চ").
                Replace("April", "এপ্রিল").Replace("May", "মে").Replace("June", "জুন").Replace("July", "জুলাই").
                Replace("August", "আগস্ট").Replace("September", "সেপ্টেম্বর").Replace("October", "অক্টোবর").Replace("November", "নভেম্বর").
                Replace("December", "ডিসেম্বর");

        }
        public static string GetMonthShortName(string name)
        {
            return name.Replace("Jan", "জানু").Replace("Feb", "ফেব্রু").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টে").Replace("Oct", "অক্টো").Replace("Nov", "নভে").
                Replace("Dec", "ডিসে");

        }

        public static string GetMonthNameDigit(string name)
        {
            return name.Replace("01", "জানুয়ারী").Replace("02", "ফেব্রুয়ারী").Replace("03", "মার্চ").
                Replace("04", "এপ্রিল").Replace("05", "মে").Replace("06", "জুন").Replace("07", "জুলাই").
                Replace("08", "আগস্ট").Replace("09", "সেপ্টেম্বর").Replace("10", "অক্টোবর").Replace("11", "নভেম্বর").
                Replace("12", "ডিসেম্বর");

        }
        public static string NumBn(string num)
        {
            string stringNum = "";

            char[] dtae = num.ToCharArray();
            foreach (var item in dtae)
            {

                switch (item)
                {
                    case '0': stringNum += "০"; break;
                    case '1': stringNum += "১"; break;
                    case '2': stringNum += "২"; break;
                    case '3': stringNum += "৩"; break;
                    case '4': stringNum += "৪"; break;
                    case '5': stringNum += "৫"; break;
                    case '6': stringNum += "৬"; break;
                    case '7': stringNum += "৭"; break;
                    case '8': stringNum += "৮"; break;
                    case '9': stringNum += "৯"; break;
                }

            }
            return stringNum;
        }



    }
}
