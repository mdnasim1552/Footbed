using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace SPELIB
{
    public sealed  class Common: System.Web.UI.Page
    {
        
        ProcessAccess purData = new ProcessAccess();
        
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        public string GetHRCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        public string GetDeptCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["deptcode"].ToString());

        }

        public string GetUserCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

        }
        public string Terminal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["compname"].ToString());

        }
        public string Sessionid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["session"].ToString());

        }
        public bool LogStatus(string eventtype, string eventdesc, string eventdesc2, string Para1)
        {
            bool IsVoucherSaved = false;
            if (ConstantInfo.LogStatus == true)
            {
                eventdesc2 = eventdesc2 + Para1;
                IsVoucherSaved = CALogRecord.AddLogRecord(this.GetCompCode(), ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
            return IsVoucherSaved;
        }

        public bool ConfimMail(string ConId, string esubject, string url, string bodyContent)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString().Trim();
            string trmid = hst["compname"].ToString().Trim();
            string usrSession = hst["session"].ToString().Trim();
            string comadd1 = hst["comadd1"].ToString().Trim();

            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

            DataSet ds = this.purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, ConId, "", "", "", "", "", "", "");

            string useremail = string.Empty;
            string useremail1 = string.Empty;

            string compname = string.Empty;


            string condate = string.Empty;
            //  string endingdate = string.Empty;

            //SMTP
            string hostname = ds.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(ds.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = Convert.ToBoolean(ds.Tables[0].Rows[0]["enblessl"]);

            string frmemail = (ds.Tables[1].Rows[0]["mailpass"].ToString() == "" || ds.Tables[1].Rows.Count == 0) ? ds.Tables[0].Rows[0]["frmmail"].ToString() : ds.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = (ds.Tables[1].Rows[0]["mailpass"].ToString() == "" || ds.Tables[1].Rows.Count == 0)? ds.Tables[0].Rows[0]["mailpass"].ToString() : ds.Tables[1].Rows[0]["mailpass"].ToString(); ;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            DataTable dt = ds.Tables[2];

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(frmemail);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                useremail = dt.Rows[i]["mailid"].ToString();
                //for (int j = 0; j < dt.Rows.Count; j++)
                //{
                //    useremail1 = dt.Rows[j]["mailid"].ToString();
                //    if (useremail == useremail1)
                //    {

                msg.To.Add(new MailAddress(useremail));
                //    }
                //}

            }
            compname = comnam;


            condate = DateTime.Today.ToString("dd.MM.yyyy");
            string body = string.Empty;
            //string tomail = useremail;

            //msg.To.Add(new MailAddress(frmemail));

            //msg.Bcc.Add(new MailAddress(tomail));


            msg.Subject = esubject;
            msg.IsBodyHtml = true;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/mail.html")))

            {

                body = reader.ReadToEnd();

            }

          

            body = body.Replace("{address}", comnam+"<br>"+ comadd1);
            body = body.Replace("{logo}", "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG");
            body = body.Replace("{msghead}", esubject);
            //body = body.Replace("{tblHead}", "Contract # ");
            //body = body.Replace("{TitelDesc1}", "Contract Subject");
            //body = body.Replace("{Desc1}", esubject);
            //body = body.Replace("{TitelDesc2}", "Company");
            //body = body.Replace("{Desc2}", "Test");
            //body = body.Replace("{TitelDesc3}", "Opening Date");
            //body = body.Replace("{Desc3}", condate);
            //body = body.Replace("{TitelDesc4}", "Ending Date");
            // body = body.Replace("{Desc4}", endingdate);



            body = body.Replace("{bodyContent}", bodyContent);

            // body = body.Replace("{address}", " Rangs Babylonia, Level 6-9, 246 Bir Uttam Mir Shawkat Road, Tejgaon I/A, Dhaka-1208, Bangladesh ");

            msg.Body = body;


            try
            {
                client.Send(msg);
                //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "teste()", true);
                //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnSuccess();", true);
                return true;

            }
            catch (Exception ex)
            {
                //
                return false;
            }


        }

        public DataSet GetShortCutLink()
        {
            return ((DataSet)Session["tblusrlog"]);
        }
        public bool PushSesLog(DataSet ds)
        {
            Session["tblusrlog"] = ds;
            return true;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public bool SendNotification(string ntitle, string ndetails, string recvid)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();         
                string username = hst["usrname"].ToString();
                string ncreatedby = hst["usrid"].ToString();
                string ncreated = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                ndetails = ndetails + " by :" + username;

                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "INSERT_NOTIFICAIOTN_USER_WISE", ntitle, ndetails, ncreated, ncreatedby, recvid, "", "");



                return true;
            }
            catch (Exception exp)
            {
               
                return false;
            }// try


        }
    }
}
