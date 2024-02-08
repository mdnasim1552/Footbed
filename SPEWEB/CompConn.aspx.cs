using SPELIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class CompConn : System.Web.UI.Page
    {
        DataAccessOLDB da = new DataAccessOLDB();
        ProcessAccess _linkVendorDb = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pnlbillalrt.Visible = false;
            this.pnlTop.Visible = false;
            this.pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                try
                {


                    string qs = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["AccessToken"].ToString()));
                    string sysID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["sysID"].ToString()));
                    string pnlType = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["sysType"].ToString()));


                    if (qs == "ptldbd2021Nahid#$CompbDb*%Process")
                    {
                        if (pnlType == "sysExp")
                        {
                            GetProcess(sysID);

                            this.pnlbillalrt.Visible = false;
                            this.pnlTop.Visible = true;
                            this.pnlmsg.Visible = false;
                        }
                        else
                        {
                            this.pnlbillalrt.Visible = true;
                            this.pnlTop.Visible = false;
                            this.pnlmsg.Visible = false;
                            GetServiceBillAltMsg(sysID);

                        }
                    }
                    else
                    {
                        //Process[] AllProcesses = Process.GetProcesses();
                        //foreach (var process in AllProcesses)
                        //{
                        //    if (process.MainWindowTitle != "")
                        //    {
                        //        string s = process.ProcessName.ToLower();
                        //        if (s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox")
                        //            process.Kill();
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Process[] AllProcesses = Process.GetProcesses();
                    //foreach (var process in AllProcesses)
                    //{
                    //    if (process.MainWindowTitle != "")
                    //    {
                    //        string s = process.ProcessName.ToLower();
                    //        if (s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox")
                    //            process.Kill();
                    //    }
                    //}
                }

            }
        }


        private void GetServiceBillAltMsg(string sysID)
        {
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            string comcod = ds1.Tables[0].Rows[0]["comcod"].ToString();
            DataSet ds2 = _linkVendorDb.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETBILLALRTMESSAGE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.txtAltMessage.Value = ds2.Tables[0].Rows[0]["COMMSG"].ToString();
            this.txtColorCode.Value = ds2.Tables[0].Rows[0]["COMMSGCOL"].ToString();
            string msgflg = ds2.Tables[0].Rows[0]["MSGFLG"].ToString();
            this.rbtLsit.SelectedValue = msgflg;

        }

        private void GetProcess(string sysID)
        {
            this.HidnsysID.Value = sysID;
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            DataSet ds3 = ulog.ExpDate();
            DataSet ds2 = ulog.GetHitCounterLimit();
            //cntstep, cntval, cntdes
            this.txt_CNUMBER.Text = ds1.Tables[0].Rows[0]["cnumber"].ToString();
            this.txt_NineFive.Text = ds2.Tables[0].Rows[0]["cntval"].ToString();
            this.txt_NineSix.Text = ds2.Tables[0].Rows[1]["cntval"].ToString();
            this.txt_NineSeven.Text = ds2.Tables[0].Rows[2]["cntval"].ToString();
            this.txtExpDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");



        }
        protected void btnUpdateSqlLimit_ServerClick(object sender, EventArgs e)
        {
            UserLogin ulog = new UserLogin();

            try
            {
                double txt_665895 = Convert.ToDouble(txt_NineFive.Text);
                double txt_665896 = Convert.ToDouble(txt_NineSix.Text);
                double txt_665897 = Convert.ToDouble(txt_NineSeven.Text);
                double cnumber = Convert.ToDouble(txt_CNUMBER.Text);
                //  string txtExpDate = Convert.ToDateTime(this.txtExpDate.Text.Trim()).ToString("dd-MM-yyyy");
                DateTime txtExpDate = Convert.ToDateTime(this.txtExpDate.Text.Trim());

                //  Hit counter Update
                ulog.UpdateHitCounter((cnumber).ToString());



                string cmd = "update hcntlmt set CNTVAL='" + txt_665895 + "' where CNTSTEP='01'";
                da.ExecuteCommand(cmd);
                string date1 = "#" + this.txtExpDate.Text + " 12:00:00 AM" + "#";
                // cmd = "update expdinf set date=" + date1 ;
                cmd = "update expdinf set [date]=" + date1;
                da.ExecuteCommand(cmd);


                cmd = "update hcntlmt set CNTVAL='" + txt_665896 + "' where CNTSTEP='02'";
                da.ExecuteCommand(cmd);
                cmd = "update hcntlmt set CNTVAL='" + txt_665897 + "' where CNTSTEP='03'";
                da.ExecuteCommand(cmd);


                string sysid = this.HidnsysID.Value.ToString();

                bool resultb = _linkVendorDb.SysExpiryUpdate(sysid, txtExpDate);
                if (resultb == true)
                {
                    this.pnlTop.Visible = false;
                    this.pnlmsg.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

                }
                else
                {
                    this.pnlTop.Visible = true;
                    this.pnlmsg.Visible = false;

                }
            }
            catch (Exception ex)
            {
                this.pnlmsg.Visible = true;
                this.msgBox.InnerText = "error" + ex;
            }

        }





        protected void btnAlrtMsg_ServerClick(object sender, EventArgs e)
        {
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            string comcod = ds1.Tables[0].Rows[0]["comcod"].ToString();

            string txtMsg = txtAltMessage.Value;
            string txtColor = txtColorCode.Value;

            string msgflg = this.rbtLsit.SelectedValue.ToString();

            bool resultb = _linkVendorDb.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEBILLALRTMESSAGE", txtMsg.ToString(), txtColor, msgflg, "", "", "", "", "");
            if (!resultb)
            {
                this.pnlbillalrt.Visible = true;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = false;
            }
            else
            {
                this.pnlbillalrt.Visible = false;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

            }
        }
    }
}