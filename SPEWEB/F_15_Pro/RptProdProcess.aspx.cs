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
using SPELIB;
using SPERDLC;
using System.Web.Script.Serialization;

namespace SPEWEB.F_15_Pro
{
    public partial class RptProdProcess : System.Web.UI.Page
    {
        ProcessAccess Prodata = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //   this.ShowOrderTrakcing();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION TRACKING";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSesson();
                this.GetCustomer();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Prodata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");


            ds1.Tables[0].Rows.Add(comcod, "00000", "--All--");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";

            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";

            }
        }

        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = Prodata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            this.DdlCustomer.DataTextField = "sirdesc";
            this.DdlCustomer.DataValueField = "sircode";
            this.DdlCustomer.DataSource = ds2.Tables[0];
            this.DdlCustomer.DataBind();
            this.DdlCustomer.SelectedValue = "000000000000";

        }


        //public void SelectView()
        //{
        //    string type = this.Request.QueryString["Type"].Trim().ToString();
        //    switch (type)
        //    {
        //        case "Graph":
        //            ProdProcessGraph();
        //            //this.MultiView1.ActiveViewIndex = 0;
        //            break;
        //    }
        //}

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ShowOrderTrakcing()
        {

            Session.Remove("tblordertk");
            string comcod = GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string fromdate = this.txtfromdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string customer = (this.DdlCustomer.SelectedValue.ToString() == "000000000000") ? "%" : this.DdlCustomer.SelectedValue.ToString() + "%"; 
            string type = (RadioButtonList2.SelectedIndex == 0 || RadioButtonList2.SelectedIndex == 1) ? "BALANCE" : "";
            DataSet ds1 = Prodata.GetTransInfo(comcod, "SP_REPORT_PRODPROCESS", "RPTORDERPROCESS", fromdate, todate, type, season, customer, "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            Session["tblprocess"] = ds1.Tables[1];
            Session["tblordertk"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string orderno = dt1.Rows[0]["orderno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["orderno"].ToString() == orderno)
                {
                    orderno = dt1.Rows[j]["orderno"].ToString();
                    dt1.Rows[j]["orderdesc"] = "";
                }

                else
                    orderno = dt1.Rows[j]["orderno"].ToString();


            }
            return dt1;

        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblordertk"];
            DataTable dt1 = (DataTable)Session["tblprocess"];
            for (int i = 3; i < this.gvOrderTrack.Columns.Count - 1; i++)
                this.gvOrderTrack.Columns[i].Visible = false;
            int j = 3;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.gvOrderTrack.Columns[j].Visible = true;
                this.gvOrderTrack.Columns[j].HeaderText = dt1.Rows[i]["prodesc"].ToString();
                if (j == 14)
                    break;
                j++;
            }

            this.gvOrderTrack.DataSource = dt;
            this.gvOrderTrack.DataBind();
            this.FooterCalculation();

            this.gvOrderTrack.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        }

        private void FooterCalculation()
        {

            DataTable tbl1 = (DataTable)Session["tblordertk"];
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp1")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p1)", "")) ?
                    0.00 : tbl1.Compute("Sum(p1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp2")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p2)", "")) ?
                    0.00 : tbl1.Compute("Sum(p2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp3")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p3)", "")) ?
                    0.00 : tbl1.Compute("Sum(p3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp4")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p4)", "")) ?
                    0.00 : tbl1.Compute("Sum(p4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp5")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p5)", "")) ?
                    0.00 : tbl1.Compute("Sum(p5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp6")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p6)", "")) ?
                    0.00 : tbl1.Compute("Sum(p6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp7")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p7)", "")) ?
                    0.00 : tbl1.Compute("Sum(p7)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp8")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p8)", "")) ?
                    0.00 : tbl1.Compute("Sum(p8)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp9")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p9)", "")) ?
                    0.00 : tbl1.Compute("Sum(p9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp10")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p10)", "")) ?
                    0.00 : tbl1.Compute("Sum(p10)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp11")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p11)", "")) ?
                    0.00 : tbl1.Compute("Sum(p11)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp12")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p12)", "")) ?
                    0.00 : tbl1.Compute("Sum(p12)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvOrderTrack.FooterRow.FindControl("lblbalinhand")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(balqty)", "")) ?
                   0.00 : tbl1.Compute("Sum(balqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblordertk"];
            //DataTable dt1 = (DataTable)Session["tblprocess"];

            //ReportDocument rptstk = new RMGiRPT.R_15_Pro.RptOrderTracking();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //txtCompany.Text = comnam;
            //int j = 1;
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{


            //    string header = dt1.Rows[i]["prodesc"].ToString();
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
            //    rpttxth.Text = header;
            //    j++;
            //    if (j == 13)
            //        break;
            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            ////  }
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void gvOrderTrack_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowOrderTrakcing();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList2.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                case "3":
                    this.divGraph.Visible = true;
                    this.gvOrderTrack.Visible = false;               
                    this.ProdProcessGraph();

                    break;

                case "0":
                case "2":
                    this.divGraph.Visible = false;
                    this.gvOrderTrack.Visible = true;           
                    break;
            }
        }

        public void ProdProcessGraph()
        {
            DataTable dt = (DataTable)Session["tblprocess"];
            DataTable dtmain = (DataTable)Session["tblordertk"];
            DataTable dtmain1 = dtmain.Clone();
            List<EClassProcessList> list1 = ASITUtility03.DataTableToList<EClassProcessList>(dt);

            dtmain1 = dtmain.AsEnumerable()
                                     .GroupBy(r => r.Field<string>("comcod"))
                                     .Select(g =>
                                     {
                                         var row = dtmain.NewRow();

                                         row["comcod"] = g.Key;
                                         row["p1"] = g.Sum(r => r.Field<decimal>("p1"));
                                         row["p2"] = g.Sum(r => r.Field<decimal>("p2"));
                                         row["p3"] = g.Sum(r => r.Field<decimal>("p3"));
                                         row["p4"] = g.Sum(r => r.Field<decimal>("p4"));
                                         row["p5"] = g.Sum(r => r.Field<decimal>("p5"));
                                         row["p6"] = g.Sum(r => r.Field<decimal>("p6"));
                                         row["p7"] = g.Sum(r => r.Field<decimal>("p7"));
                                         row["p8"] = g.Sum(r => r.Field<decimal>("p8"));
                                         row["p9"] = g.Sum(r => r.Field<decimal>("p9"));
                                         row["p10"] = g.Sum(r => r.Field<decimal>("p10"));
                                         row["p11"] = g.Sum(r => r.Field<decimal>("p11"));
                                         row["p12"] = g.Sum(r => r.Field<decimal>("p12"));
                                         row["p13"] = g.Sum(r => r.Field<decimal>("p13"));
                                         return row;
                                     }).CopyToDataTable();


            int i = 1;
            foreach (var item in list1)
            {
                item.qty = Convert.ToDouble("0" + dtmain1.Rows[0]["p" + i]);
                i++;
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var process = jsonSerialiser.Serialize(list1);
            var maindata = jsonSerialiser.Serialize(list1);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + process + "','" + maindata + "')", true);

        }

    }

    public class EClassProcessList
    {
        public string prodesc { get; set; }
        public double qty { get; set; }
        public EClassProcessList() { }
    }


}