using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using SPELIB;
using System.Net;

namespace SPELIB
{

    public class SendSmsProcess : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        private Hashtable _errObj;

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        public bool SendSmms(string text, string userid, string frmname)
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
                string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
                string SMSText = text; //        
                string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
                string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
                for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
                {
                    string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + sender
                       + "&SMSText=" + SMSText + "&GSM=" + mobile + "&type=longSMS");

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                }


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }




        public bool SendSmmsPwd(string comcode, string text, string mobilenum)
        {
            try
            {
                string comcod = comcode;
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOFORFORGOTPASS","", "", "", "", "");
                string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
                
                string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
                string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
              
                    string mobile = "88" +mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + sender
                       + "&SMSText=" + text + "&GSM=" + mobile + "&type=longSMS");

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
            


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }



        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }
    }
}
