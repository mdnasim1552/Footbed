using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPELIB;
using System.IO;
using System.Data.OleDb;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace SPEWEB.F_05_ProShip
{
    public partial class MasterCalendarSetup : System.Web.UI.Page
    {
        ProcessAccess objdata = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                 ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtDateto.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString();
                if (this.Request.QueryString["date"].Length > 0)
                {
                    this.txtDatefrom.Text = Convert.ToDateTime(this.Request.QueryString["date"].ToString()).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = Convert.ToDateTime(this.Request.QueryString["dayid"].ToString()).ToString("dd-MMM-yyyy");

                }
                ((Label)this.Master.FindControl("lblTitle")).Text =(type== "mstrcalendar")?"Master Calender Setup":"Production Planning Calendar Setup";
                CommonButton();
                if(type!= "mstrcalendar")
                {
                    GetProcess();
                    this.ddlProcess.Visible = true;
                    this.lblProcess.Visible = true;
                    this.LblHour.Visible = true;
                    this.TxtHours.Visible = true;
                    this.LbtnPopulate.Visible = true;
                }

                

            }
        }
        private void GetProcess()
        {
            Session.Remove("tblprocess");
            string comcod = GetComeCode();
            DataSet ds1 = objdata.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("procode <>'800100101099'");
            dt = dv.ToTable();
            dt.Rows.Add("000000000000","None");
            this.ddlProcess.DataTextField = "prodesc";
            this.ddlProcess.DataValueField = "procode";
            this.ddlProcess.DataSource = dt;
            this.ddlProcess.DataBind();

          
           
                this.ddlProcess.SelectedValue = "000000000000";

            Session["tblprocess"] = ds1.Tables[0];
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.lbtnOk_Click(null,null);
            }
            this.ddlProcess_SelectedIndexChanged(null, null);

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }
        private void CommonButton()
        {

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
              ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = (this.Request.QueryString["Type"].ToString() == "plancalendar") ? true : false; ;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblcalendar"];

            for (int i = 0; i < this.gvMasterCalendar.Rows.Count; i++)
            {
                string dstatus = ((DropDownList)this.gvMasterCalendar.Rows[i].FindControl("DdlgvDayType")).SelectedValue.ToString();
                string remarks = ((TextBox)this.gvMasterCalendar.Rows[i].FindControl("lblgvRemarks")).Text.Trim();

                dt.Rows[i]["dstatus"] = dstatus;
                dt.Rows[i]["remarks"] = remarks;

            }

            ViewState["tblcalendar"] = dt;

        }

        private void Save_Value_Plan()
        {
            DataTable dt = (DataTable)ViewState["tblcalendar"];

            for (int i = 0; i < this.gvplan.Rows.Count; i++)
            {
                string altstatus = ((DropDownList)this.gvplan.Rows[i].FindControl("DdlgvAltDaytype")).SelectedValue.ToString();
                string notes = ((TextBox)this.gvplan.Rows[i].FindControl("TxtNotes")).Text.ToString();

                dt.Rows[i]["altstatus"] = altstatus;
                dt.Rows[i]["notes"] = notes;

            }

            ViewState["tblcalendar"] = dt;

            DataTable tblline = (DataTable)ViewState["tblline"];

            for (int i = 0; i < this.gvlines.Rows.Count; i++)
            {
                double wrkhours = Convert.ToDouble("0"+((TextBox)this.gvlines.Rows[i].FindControl("lblgvHours")).Text);

                tblline.Rows[i]["wrkhours"] = wrkhours;

            }
            ViewState["tblline"] = tblline;
        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "mstrcalendar")
            {
                this.Save_Value();
                DataTable dt = (DataTable)ViewState["tblcalendar"];
                string comcod = this.GetComeCode();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dayid = dt.Rows[i]["dayid"].ToString();
                    string dstatus = dt.Rows[i]["dstatus"].ToString();
                    string remarks = dt.Rows[i]["remarks"].ToString();


                     result = objdata.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_MASTER_CALENDAR", dayid, dstatus, remarks);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + objdata.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


                    }


                }

            }
            else
            {
                this.Save_Value_Plan();
                DataTable dt = (DataTable)ViewState["tblcalendar"];
                DataTable tblline = (DataTable)ViewState["tblline"];
                string comcod = this.GetComeCode();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dayid = dt.Rows[i]["dayid"].ToString();
                    string altstatus = dt.Rows[i]["altstatus"].ToString();
                    string notes = dt.Rows[i]["notes"].ToString();

                    if (altstatus == "WRK") { ////only working day plan updated
                    foreach (DataRow item in tblline.Rows)
                    {
                        string process = item["prodprocess"].ToString();
                        string linecod = item["sircode"].ToString();
                        string wrkhours = item["wrkhours"].ToString();

                        result = objdata.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_PLAN_CALENDAR", dayid, process, linecod,wrkhours, altstatus, notes);
                    }
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + objdata.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


                    }
                    }

                }
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
             this.lbtnOk.Text = "New";
            string type = this.Request.QueryString["Type"].ToString();
            if(type== "mstrcalendar")
            {
                this.Multiview1.ActiveViewIndex = 0;
                this.MasterCalendar();
            }
            else
            {
                this.Multiview1.ActiveViewIndex = 1;
                this.ddlProcess.Enabled = false;
                PlanCalendar();
                    this.txtFromDt2.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
                    this.txtToDt2.Text = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

                }
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.gvMasterCalendar.DataSource = null;
                this.gvMasterCalendar.DataBind();
                this.gvplan.DataSource = null;
                this.gvplan.DataBind();

                this.gvlines.DataSource = null;
                this.gvlines.DataBind();
                this.ddlProcess.Enabled = true;
            }

        }

        private void MasterCalendar()
        {
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            ViewState.Remove("tblcalendar");
            DataSet ds1 = objdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_MASTER_CALENDAR_INFO", fromdate, todate,
                          "", "", "", "", "", "", "");
            ViewState["tblcalendar"] = ds1.Tables[0];
            Data_Bind();
        }

        private void PlanCalendar()
        {
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string process =(this.ddlProcess.SelectedValue.ToString()=="000000000000")?"%": this.ddlProcess.SelectedValue.ToString()+"%";
            string linecode = "%";
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                linecode = this.Request.QueryString["sircode"] + "%";
            }
            ViewState.Remove("tblcalendar");
            DataSet ds1 = objdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_PLAN_CALENDAR_INFO", fromdate, todate,
                          process, linecode, "", "", "", "", "");
            ViewState["tblcalendar"] = ds1.Tables[0];
            ViewState["tblline"] = ds1.Tables[1];
            Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblcalendar"];

            string type = this.Request.QueryString["Type"].ToString();
            if (type == "mstrcalendar")
            {
                this.gvMasterCalendar.DataSource = dt;
                this.gvMasterCalendar.DataBind();
            }
            else
            {
                DataTable tblline = (DataTable)ViewState["tblline"];
                this.gvplan.DataSource = dt;
                this.gvplan.DataBind();

                this.gvlines.DataSource = tblline;
                this.gvlines.DataBind();
            }


        }

        protected void gvMasterCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string dstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dstatus"));
                DropDownList ddldaytype = (DropDownList)e.Row.FindControl("DdlgvDayType");

                ddldaytype.SelectedValue = dstatus;

                if (dstatus != "WRK")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvplan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string dstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dstatus"));
                string altstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "altstatus"));

                if (dstatus != "WRK")
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }

                DropDownList ddldaytype = (DropDownList)e.Row.FindControl("DdlgvAltDaytype");

                ddldaytype.SelectedValue = altstatus;

                if (altstatus != "WRK")
                {
                    ddldaytype.ForeColor= System.Drawing.Color.Red;
                }
            }
        }

        protected void LbtnPopulate_Click(object sender, EventArgs e)
        {
            DataTable tblline = (DataTable)ViewState["tblline"];
            double hours = Convert.ToDouble("0"+ this.TxtHours.Text);
            if (hours == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Input Hourse Before Populate');", true);

                return;
            }
            foreach (DataRow item in tblline.Rows)
            {
                item["wrkhours"] = hours;
            }

            ViewState["tblline"] = tblline;
            this.TxtHours.Text = String.Empty;
            this.Data_Bind();
        }

        protected void lblgvAllocatedHours_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string dayid = ((Label)this.gvplan.Rows[index].FindControl("Lbldayid")).Text.ToString();
            string process = (this.ddlProcess.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProcess.SelectedValue.ToString() + "%";
            ViewState.Remove("tbldaydetails");
            DataSet ds1 = objdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_PLAN_CALENDAR_DAYWISE_DETAILS", dayid, process,"", "", "", "", "", "");
            ViewState["tbldaydetails"] = ds1.Tables[0];
            this.gvDayWiseDeails.DataSource = ds1.Tables[0];
            this.gvDayWiseDeails.DataBind();
            this.gvDaywisePlan.DataSource = null;
            this.gvDaywisePlan.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lbtnPlnModfied_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldaydetails"];
            bool result = false;
            string comcod = this.GetComeCode();
            for (int i = 0; i < this.gvDayWiseDeails.Rows.Count; i++)
            {
                double wrkhours = Convert.ToDouble("0"+ ((TextBox)this.gvDayWiseDeails.Rows[i].FindControl("txtgvWrkHours")).Text);
                string altdaytype = dt.Rows[i]["altdaytype"].ToString();
                string process = dt.Rows[i]["process"].ToString();
                string linecod = dt.Rows[i]["linecod"].ToString();
                string dayid = dt.Rows[i]["dayid"].ToString();
                string notes = dt.Rows[i]["notes"].ToString();

                result = objdata.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_PLAN_CALENDAR", dayid, process, linecod, wrkhours.ToString(), altdaytype, notes);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + objdata.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);

        }

        [WebMethod]
        public static List<SPEENTITY.C_05_ProShip.EClassPlanning.EclassFullYearEvents> GetFullYearEvent(string yearid)
        {
            Common com = new Common();
            string comcod = com.GetCompCode();
            ProcessAccess objdata1 = new ProcessAccess();
            DataSet ds1 = objdata1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_FULL_YEAR_EVENTS", yearid, "", "", "", "", "", "");
            List<SPEENTITY.C_05_ProShip.EClassPlanning.EclassFullYearEvents> list = ds1.Tables[0].DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.EclassFullYearEvents>();
            return list;

        }

        protected void lblgvDate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string dayid = ((Label)this.gvplan.Rows[index].FindControl("Lbldayid")).Text.ToString();
            string process = (this.ddlProcess.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProcess.SelectedValue.ToString() + "%";
            string linecode = (this.Request.QueryString["sircode"].Length==0) ? "%" : this.Request.QueryString["sircode"].ToString() + "%";

            ViewState.Remove("tbldayplansum");
            DataSet ds1 = objdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_DAYWISE_DETAILS_PLAN", dayid, process, linecode, "", "", "", "", "");
            ViewState["tbldayplansum"] = ds1.Tables[0];
            this.gvDaywisePlan.DataSource = ds1.Tables[0];
            this.gvDaywisePlan.DataBind();
            this.gvDayWiseDeails.DataSource = null;
            this.gvDayWiseDeails.DataBind();
            this.lbtnPlnModfied.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void SaveLineDateRange_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string linecode = this.TxtLineCode.Text.ToString();
            string fromdate = Convert.ToDateTime(this.txtFromDt2.Text).ToString();
            string todate = Convert.ToDateTime(this.txtToDt2.Text).ToString();
            string hours = this.txtHourDt2.Text.ToString();

            bool result = objdata.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_LINE_DATE_RANGE_HOURS", linecode, fromdate, todate, hours, "", "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Plan Update Successfully');", true);
                this.PlanCalendar();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + objdata.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }
        }
    }
}

