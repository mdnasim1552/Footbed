﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using SPELIB;


namespace SPEWEB.F_34_Mgt
{
    public partial class RptGroupTask : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        //  UserManMkt objuserman = new UserManMkt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Uase Task  Discussion Details";
                this.GetProjectAUnit();
                this.GetProfession();
                this.GetIntlocation();
                this.GetGrade();
                this.GetUserInfo();
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProfession()
        {
            //string comcod = this.GetCompCode();

            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetProAndLocatio(comcod);
            //var lst1 = lst.FindAll(l => l.gcod.Substring(0, 4) == "8601");
            //this.ddlprofession.DataTextField = "gdesc";
            //this.ddlprofession.DataValueField = "gcod";
            //this.ddlprofession.DataSource = lst1;
            //this.ddlprofession.DataBind();
            //ViewState["tblProfess"] = lst1;


        }
        private void GetProjectAUnit()
        {
            //ViewState.Remove("tblproaunit");
            //string comcod = GetCompCode();
            //DataSet dss = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETPROJECTAUNIT", "", "", "", "", "", "", "", "", "");
            //ViewState["tblproaunit"] = dss;
            //this.ddlproject.DataTextField = "pactdesc";
            //this.ddlproject.DataValueField = "pactcode";
            //this.ddlproject.DataSource = dss.Tables[0];
            //this.ddlproject.DataBind();

            //dss.Dispose();
        }
        private void GetIntlocation()
        {
            //ViewState.Remove("tbllocation");
            //string comcod = GetCompCode();
            //DataSet dt11 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "MKTINTLOCATION", "", "", "", "", "", "", "", "", "");
            //DataTable dt = dt11.Tables[0];
            //this.ddllocation.DataTextField = "gdesc";
            //this.ddllocation.DataValueField = "gcod";
            //this.ddllocation.DataSource = dt;
            //this.ddllocation.DataBind();
            //ViewState["tbllocation"] = dt;

        }
        private void GetGrade()
        {
            //ViewState.Remove("tblgrade");
            //string comcod = GetCompCode();
            //DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "MKTIGRADE", "", "", "", "", "", "", "", "", "");
            //DataTable dt11 = ds1.Tables[0];
            //this.ddlgrade.DataTextField = "gdesc";
            //this.ddlgrade.DataValueField = "gcod";
            //this.ddlgrade.DataSource = dt11;
            //this.ddlgrade.DataBind();
            //ViewState["tblgrade"] = dt11;

        }
        private void GetUserInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");


            DataSet gtctaks = MktData.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETCHATDATA", "", "", "", "", "", "", "", "", "");
            if (gtctaks == null)
                return;

            var userinfo = gtctaks.Tables[0].DataTableToList<SPEENTITY.C_34_Mgt.Grouptaskuser>();
            var userdetails = gtctaks.Tables[1].DataTableToList<SPEENTITY.C_34_Mgt.Grouptaskchat>();
            ViewState["userinfo"] = userinfo;
            ViewState["userdetails"] = userdetails;




            this.Data_Bind();


            // this.totalcustomer.InnerText =  " ( " + dskpi.Tables[0].Rows.Count.ToString() + " ) ";





            //for (int j = 0; j < repteclientdetail.items.count; j++) //old reject
            //{
            //    string proscod = (string)dskpi.Tables[0].Rows[j]["proscod"];
            //    //string curSymbol = (string)ds1.Tables[0].Rows[j]["currency"];
            //    //PLZ CONTROL 
            //    DataTable dt1 = new DataTable();
            //    DataView view1 = new DataView();
            //    view1.Table = dskpi.Tables[1];
            //    view1.RowFilter = "proscod = '" + proscod + "'";
            //    dt1 = view1.ToTable();


            //        repDiscdata.DataSource = dt1;
            //        repDiscdata.DataBind();

            ////string curSymbol = ds1.Tables[0].Rows[];//"  <span class='fa fa-rupee primary'></span>  ";

            //var count = dt1.Rows.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    if (i + 2 >= 7)
            //        break;
            //    string lbl = "Label" + (i + 2).ToString();
            //    ((Label)this.repteClientDetail.Items[j].FindControl(lbl)).Text = dt1.Rows[i]["prosdesc"].ToString();
            //}
            //string lbl2 = ((Label)this.repteClientDetail.Items[j].FindControl("Label1")).Text.ToString();

            //((HyperLink)this.repteClientDetail.Items[j].FindControl("btnLink")).Attributes.Add("href", "Javascript:AddCartContent(" + proscod + "," + lbl2 + ")");

            //}




        }


        protected void repUserDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            List<SPEENTITY.C_34_Mgt.Grouptaskchat> list4 = (List<SPEENTITY.C_34_Mgt.Grouptaskchat>)ViewState["userdetails"];
            DataTable dt = ASITUtility03.ListToDataTable(list4);
            dt.TableName = "Table1";
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    DataTable Wdt = new DataTable();
                    Label lbl_userid = (Label)e.Item.FindControl("Label1");
                    HyperLink hyp = (HyperLink)e.Item.FindControl("hyplink") as HyperLink;
                    HyperLink HypView = (HyperLink)e.Item.FindControl("HypView") as HyperLink;


                    string usercod = lbl_userid.Text;

                    Repeater Chi_Repeat = (Repeater)e.Item.FindControl("repUserDetails");

                    DataView view1 = new DataView();
                    view1.Table = dt;
                    view1.RowFilter = "userid = '" + usercod + "'";
                    Wdt = view1.ToTable();

                    DataTable dt1 = (Wdt.AsEnumerable().Take(3).CopyToDataTable());


                    //DataRow[] rows = Wdt.Select("TOP 2");
                    Chi_Repeat.DataSource = dt1;
                    Chi_Repeat.DataBind();

                    hyp.NavigateUrl = "../F_21_MKT/MktEmpKpiEntry.aspx?Type=Entry&clientid=" + usercod;
                    HypView.NavigateUrl = "../F_21_MKT/ClientProfileView.aspx?Type=Mgt&clientid=" + usercod;



                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            //string id = (((sender as LinkButton).NamingContainer as RepeaterItem).FindControl("Label1") as Label).Text;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //string comcod = hst["comcod"].ToString();

            //bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "INSERTCLIENTSTATUS", id,"False");
            //if(!result)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + MktData.ErrorObject["Msg"];
            //    return;
            //}
            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "successfully client status disable";
            //    GetClientDiscussuion();
            //}
        }

        protected void lnkbtnEmail_Click(object sender, EventArgs e)
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string usrRole = hst["roleid"].ToString();

            //    var row = ((Button)sender).NamingContainer;
            //    Label getsup = (Label)row.FindControl("Label1");
            //    //var supid = getsup.Text.ToString();
            //    //var lst = GetCatWiseProd.GetCompanyInfo(supid);
            //    //var getemail = lst.Find(p => p.gcod == "71051");

            //    //this.sup_email.Text = getemail.gdatat.ToString();
            //    //this.sup_email.Enabled = false;

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);



        }



        private void Data_Bind()
        {
            List<SPEENTITY.C_34_Mgt.Grouptaskuser> userinfo1 = (List<SPEENTITY.C_34_Mgt.Grouptaskuser>)ViewState["userinfo"];
            if (userinfo1.Count() == 0)
            {
                repteUserinfo.DataSource = null;
                repteUserinfo.DataBind();
                return;
            }
            repteUserinfo.DataSource = userinfo1;
            repteUserinfo.DataBind();
        }

        protected void SubmitMsg_OnClick(object sender, EventArgs e)
        {

            ////SMTP
            //string hostname = "mail.realprice.biz"; //dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            //int portnumber = 25;// Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            //SmtpClient client = new SmtpClient(hostname, portnumber);
            ////SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////client.EnableSsl = true;
            //client.EnableSsl = false;
            //string frmemail = "support@realprice.biz";// dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            //string psssword = "support**987";// dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;
            /////////////////////////

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress(frmemail);


            //string body = string.Empty;
            //string tomail = this.sup_email.Text.ToString();

            //msg.To.Add(new MailAddress(tomail));

            //msg.Subject = this.subject.Text.ToString();
            //msg.IsBodyHtml = true;
            //using (StreamReader reader = new StreamReader(Server.MapPath("~/Buyer/sup_msg.html")))
            //{

            //    body = reader.ReadToEnd();

            //}
            //body = body.Replace("{subject}", this.subject.Text.ToString()); //replacing the required things  

            //body = body.Replace("{msg}", this.msgbody.Text.ToString());
            ////      body = body.Replace("{url}", url);

            ////  body = body.Replace("{message}", message);
            //msg.Body = body;

            //// msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Hello," + UsrName + "<br/>" + "please Collect Your Password " + rndnumber + "</pre></body></html>");
            //try
            //{
            //    client.Send(msg);
            //    this.succ_alert.Visible = true;
            //}
            //catch (Exception ex)
            //{
            //}


        }

        protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filter = this.ddlfilter.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> shortedlist = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];
            //switch (filter)
            //{
            //    case "name":
            //        this.OtherFilter.Visible = false;
            //         shortedlist = clientdetails.OrderBy(x => x.prosdesc).ToList();
            //        break;
            //    case "recentadded":
            //        this.OtherFilter.Visible = false;
            //        shortedlist = clientdetails.OrderByDescending(x => x.createdate).ToList();
            //        break;

            //    case "nextapoin":
            //        this.OtherFilter.Visible = false;
            //        shortedlist.Clear();
            //        List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientDiscuss> list3 = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientDiscuss>)ViewState["tblDiscuss"];
            //       var  nextapntdate = list3.OrderByDescending(x => x.napnt).Select(p => p.proscod).Distinct().ToList();
            //        for (int i= 0; i< nextapntdate.Count(); i++)
            //        {
            //            List < RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails > findata = clientdetails.FindAll(t => t.proscod == nextapntdate[i]);
            //            shortedlist.Add(new RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails(findata[0].prosdesc,
            //               findata[0].proscod, findata[0].phone, findata[0].createdate, findata[0].profesdesc, findata[0].locdes, findata[0].grddesc,
            //               findata[0].email, findata[0].visited, findata[0].clstatus, findata[0].imgpath, findata[0].professcode,
            //               findata[0].intlocation, findata[0].grade));
            //        }

            //        var filteredOrders = clientdetails.Where(o => nextapntdate.Contains(o.proscod));
            //        var SortedItems = clientdetails.OrderBy(i => nextapntdate.IndexOf(i.proscod));
            //        //  shortedlist = clientdetails.ForEach(a => a.list3.Sort((x, y) => x. - y.Id));
            //        break;


            //    default:
            //        this.OtherFilter.Visible = true;
            //        shortedlist = clientdetails;
            //        break;
            //}

            //ViewState["ClientDetails"]= shortedlist;
            //this.Data_Bind();
        }

        protected void ddlprofession_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filter = this.ddlprofession.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];
            //this.repteClientDetail.DataSource = clientdetails.FindAll(x => x.professcode== filter).ToList();
            //this.repteClientDetail.DataBind();
            ////this.Data_Bind();
        }

        protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filter = this.ddllocation.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];
            //this.repteClientDetail.DataSource = clientdetails.FindAll(x => x.intlocation == filter).ToList();
            //this.repteClientDetail.DataBind();
        }

        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filter = this.ddlgrade.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];
            //this.repteClientDetail.DataSource = clientdetails.FindAll(x => x.grade == filter).ToList();
            //this.repteClientDetail.DataBind();
        }

        protected void ddlvisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filter = this.ddlvisit.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];
            //this.repteClientDetail.DataSource = clientdetails.FindAll(x => x.visited == filter).ToList();
            //this.repteClientDetail.DataBind();
        }

        protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string proejct = this.ddlproject.SelectedValue.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> shortedlist = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> clientdetails = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails>)ViewState["ClientDetails"];

            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientDiscuss> list3 = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientDiscuss>)ViewState["tblDiscuss"];
            //var nextapntdate = list3.FindAll(x => x.pactcode== proejct).Select(p => p.proscod).Distinct().ToList();
            //for (int i = 0; i < nextapntdate.Count(); i++)
            //{
            //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails> findata = clientdetails.FindAll(t => t.proscod == nextapntdate[i]);
            //    shortedlist.Add(new RealEntity.C_21_Mkt.EClassAdvertisement.EclassClientdetails(findata[0].prosdesc,
            //       findata[0].proscod, findata[0].phone, findata[0].createdate, findata[0].profesdesc, findata[0].locdes, findata[0].grddesc,
            //       findata[0].email, findata[0].visited, findata[0].clstatus, findata[0].imgpath, findata[0].professcode,
            //       findata[0].intlocation, findata[0].grade));
            //}
            //this.repteClientDetail.DataSource = shortedlist;
            //this.repteClientDetail.DataBind();
        }
    }
}