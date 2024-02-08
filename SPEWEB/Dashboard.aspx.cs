using SPEENTITY;
using SPEENTITY.C_10_Procur;
using SPEENTITY.C_15_Pro;
using SPEENTITY.C_23_MAcc;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{

    public partial class Dashboard : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();
        static List<EmployeeCounter>  elist= new List<EmployeeCounter>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP SMARTBOARD";
                this.Show_Events();               
               
                if (GetRoleId() == "01")
                {                    
                    this.CounterPanel.Visible = false;
                    this.DashboardSummary.Visible = false;
                    this.Grapharea.Visible = false;
                    this.GetShortCut();
                }
                else
                {
                    this.divUserPanel.Visible = false;
                    this.ShowData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetData()", true);

                }



            }
        }

        public string GetRoleId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return hst["userrole"].ToString();

        }

        private void GetShortCut()
        {
            string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            DataSet ds = ((DataSet)Session["tblusrlog"]);
            DataTable dt1 = ds.Tables[1];
            DataTable shrtcuttble = ds.Tables[1].Clone();
            DataTable dt3 = ds.Tables[3];
            if (dt3.Rows.Count == 0 || dt3 == null)
                return;
            foreach (DataRow dr in dt3.Rows)
            {
                DataRow[] srows = dt1.Select("frmid = '" + dr["formid"].ToString().Trim() + "'");
                if (srows.Length > 0)
                {
                    shrtcuttble.ImportRow(srows[0]);

                }
            }
            DataTable newdt = shrtcuttble;

            string MyShortCut = "";

            foreach (DataRow dr in shrtcuttble.Rows)
            {
                MyShortCut += @"<li class='menu-item'><a href ='" + path + "/" + dr["floc"] + "/" + dr["urlinf"] + dr["qrytype1"] + "' class='menu-link' target='_self'>" + dr["dscrption"] + "</a></li>";
            }

            this.ShorCut.InnerHtml = MyShortCut;

        }
        //fahad Test merger,sdfghj
        private void ShowData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");

            string fastdate = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");

            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GET_NEW_INFO", fastdate, tdate, "", "", "", "", "");
            ViewState["NewJoin"] = ds2.Tables[0];
            elist= ds2.Tables[0].DataTableToList<EmployeeCounter>();

            if (ds1 == null)
                return;

            this.todaywrkcount.InnerHtml = ds1.Tables[2].Rows[0]["tcount"].ToString();
            this.TaskRemaining.InnerHtml = ds1.Tables[2].Rows[0]["tasks"].ToString();
            this.MasterOrder.InnerHtml = ds1.Tables[2].Rows[0]["masterorder"].ToString();
            this.todaywrk.Attributes.Add("href", "F_34_Mgt/RptUserLogDetails?Type=Entry&genno=" + usrid);
            if (ds1.Tables[3] == null || ds1.Tables[3].Rows.Count == 0)
                return;
            string innHTML = "";
            int i = 0;
            foreach (DataRow dr in ds1.Tables[3].Rows)
            {
                string url1 = "";

                if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                {

                    byte[] ifff = (byte[])dr["usrimg"];
                    url1 = "data:image;base64," + Convert.ToBase64String(ifff);
                }
                else
                {
                    url1 = "Content/Theme/images/avatars/human_avatar.png";
                }
                i++;
                innHTML += @"<div class='card mb-2'>" +
                        "<div class='card-body'>" +
                          "<div class='row align-items-center'>" +
                            "<div class='col-auto'>" +
                              "<a href = 'user-profile.html' class='user-avatar user-avatar-lg'><img src = '" + url1 + "' alt=''><span class='avatar-badge offline' title='offline'></span></a> </div>" +
                            "<div class='col'>" +
                              "<h3 class='card-title'>" +
                                "<a href = 'user-profile.html' >" + dr["usrname"] + "</a> <small class='text-muted'>@</small></h3>" +
                              "<h6 class='card-subtitle text-muted'>" + dr["usrdesig"] + "</h6> </div>" +
                            "<div class='col-auto'>" +
                              "<button type = 'button' class='btn btn-icon btn-secondary mr-1' data-toggle='tooltip' title='' data-original-title='Private message'><i class='far fa-comment-alt'></i></button>" +
                              "<div class='dropdown d-inline-block'>" +
                                "<button class='btn btn-icon btn-secondary' data-toggle='dropdown'><i class='fa fa-fw fa-ellipsis-h'></i></button>" +
                                "<div class='dropdown-menu dropdown-menu-right'>" +
                                  "<div class='dropdown-arrow'></div><button type = 'button' class='dropdown-item'>Invite to a team</button> <button type = 'button' class='dropdown-item'>Copy member ID</button>" +
                                  "<div class='dropdown-divider'></div><button type = 'button' class='dropdown-item'>Remove</button>" +
                                "</div> </div>  </div>  </div>  </div>  </div>";


            }
            this.offlineUserCount.InnerHtml = Convert.ToString(i);
            this.OfflineUsers.InnerHtml = innHTML;
            //----------------Top Order-------------------///
            string topOrder = "";
            foreach (DataRow dr in ds2.Tables[1].Rows)
            {
                //byte [] ifff=;
                string urlord = "";

                //if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                //{

                //    byte[] ifff = (byte[])dr["usrimg"];
                //    url = "data:image;base64," + Convert.ToBase64String(ifff);
                //}
                //else
                //{
                //    url = "Content/Theme/images/avatars/human_avatar.png";
                //}

                urlord = "Content/Theme/images/avatars/human_avatar.png";




                topOrder += @"<a href='#' class='list-group-item list-group-item-action'>" +
                              "<div class='list-group-item-figure'>" +
                                "<div class='user-avatar'>" +
                                  "<img src ='" + urlord + "' alt=''></div></div>" +

                              "<div class='list-group-item-body'>" +
                                "<h4 class='list-group-item-title font-size-sm'> " + dr["buyername"] + "</h4>" +
                                "<p class='list-group-item-text text-truncate'>" +
                                "<span class='text-dark font-size-sm'>" + dr["gpdesc"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span> </p>" +
                              "</div></a>";
            }
            TopOrderSt.InnerHtml = topOrder;


            //------------------------------------------//
            string toactivity = "";
            foreach (DataRow dr in ds1.Tables[4].Rows)
            {
                //byte [] ifff=;
                string url = "";

                if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                {

                    byte[] ifff = (byte[])dr["usrimg"];
                    if (ifff.Length > 0)
                    {
                        url = "data:image;base64," + Convert.ToBase64String(ifff);

                    }
                    else
                    {
                        url = "Content/Theme/images/avatars/human_avatar.png";

                    }
                   
                }
                else
                {
                    url = "Content/Theme/images/avatars/human_avatar.png";
                }



                //  Response.BinaryWrite(ifff);


                toactivity += @"<a href='#' class='list-group-item list-group-item-action'>" +
                              "<div class='list-group-item-figure'>" +
                                "<div class='user-avatar'>" +
                                  "<img src ='" + url + "' alt=''></div></div>" +

                              "<div class='list-group-item-body'>" +
                                "<h4 class='list-group-item-title font-size-sm'> " + dr["usersname"] + "</h4>" +
                                "<p class='list-group-item-text text-truncate'>" +
                                "<span class='text-dark font-size-sm'>" + dr["usrdesig"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span> </p>" +
                              "</div></a>";
            }
            TopActivity.InnerHtml = toactivity;



           // List<EmployeeCounter> lst = ds2.Tables[0].DataTableToList<EmployeeCounter>();
           // var jsonSerialiser = new JavaScriptSerializer();
           // var jsondata = jsonSerialiser.Serialize(lst);
           //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GenerateGraph('" + jsondata + "')", true);

        }
        private void Get_Events()
        {
            string comcod = this.GetCompCode();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            Hashtable hst = (Hashtable)Session["tblLogin"];           
            string userid = hst["usrid"].ToString();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "GET_UPCOMMING_EVENTS", fdate, userid, "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            string innHTML = "";
            int i = 0;
            string status = "";
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                status = (i == 0) ? "active" : "";
                innHTML += @"<div class='carousel-item " + status + "'><div class='row'><div class='col-md-1'><a href ='#' class='font-size-sm'><span class='position-relative mx-2 badge badge-primary rounded-0 '>" + dr["evtype"] + "</span></a></div><div class='col-md-10'> <a class='label font-size-sm' href='#'>" + dr["eventitle"] + "</a></div></div></div>";
                i++;
            }
            this.EventCaro.InnerHtml = innHTML;
        }
        private void Show_Events()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            UserName.InnerHtml = "Hi, " + hst["usrname"].ToString();

            if (hst["events"].ToString() == "True")
            {
                this.Get_Events();
                EventNotice.Visible = true;
            }
            else
            {
                EventNotice.Visible = false;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string dates)

        {
            try
            {
               // string dates = "25-May-2021";
                Common ObjCommon = new Common();
                string comcod = ObjCommon.GetCompCode();
                UserManOrder objUserService = new UserManOrder();
                UserManPur objUserServicePur = new UserManPur();
                BL_Production objUserServProduction = new BL_Production();
                UserDB_BL objUserServiceACC = new UserDB_BL();
                ProcessAccess _DataEntry = new ProcessAccess();
                List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, dates);//sales
                List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> list2 = objUserServicePur.ShowPurMonth(comcod, dates);// purchase
                List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> lst3 = objUserServProduction.ShowMonthly(comcod, dates); //production
                List<SPEENTITY.C_23_MAcc.EClassDB_BO.EClassAccMonthly> list4 = objUserServiceACC.ShowMonthlyAcc(dates); //acccounts
                DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", dates, "", "", "", "", "", "", "");
                List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum>();
                //   return "hello Safi"+Convert.ToDateTime(dates).ToString("dd-MM-yyyy");
                var balsheetlist = lst5.FindAll(p => p.grp == "2");
                //  return js;
                //  var result = lst1.Concat(list2);

                //// new employee

                var jsonSerialiser = new JavaScriptSerializer();

               
                var datalist = new MyallDataCombind(lst1, list2, lst3, list4, balsheetlist, elist);

             
                var json = jsonSerialiser.Serialize(datalist);
                //var json = jsonSerialiser.Serialize(list2);
                return json;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }

    [Serializable]
    public class EmployeeCounter
    {
        public string deptcode { get; set; }
        public string deptname { get; set; }
        public string empcount { get; set; }
        public EmployeeCounter() { }
    }







    public class MyallDataCombind
    {
        public List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> sales { get; set; }
        public List<EClassPur.EClassMonthly> pur { get; set; }
        public List<BO_Production.EClassMonthly> prod { get; set; }
        public List<EClassDB_BO.EClassAccMonthly> acc { get; set; }
        public List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> balshet { get; set; }
        public List<EmployeeCounter> elist { get; set; }
        public MyallDataCombind() { }
        public MyallDataCombind(List<SPEENTITY.C_31_Mis.EClassOrder.EClassMonthly> sales,
            List<EClassPur.EClassMonthly> pur, List<BO_Production.EClassMonthly> prod,
            List<EClassDB_BO.EClassAccMonthly> acc, 
            List<SPEENTITY.C_31_Mis.EClassOrder.EclassBalSheetSum> balshet,
            List<EmployeeCounter> elist)
        {
            this.sales = sales;
            this.pur = pur;
            this.prod = prod;
            this.acc = acc;
            this.balshet = balshet; 
            this.elist = elist;
        }
    }
}