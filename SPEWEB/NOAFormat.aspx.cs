using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB
{
    public partial class NOAFormat : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CommonButton();
                var type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = date;

                ShowView();
            }
        }



        private void CommonButton()
        {

            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnsave_Click);


            //  ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnSave_Click);


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }




        protected void btnsave_Click(object sender, EventArgs e)
        {


            var strval = this.txtml.Text;



            string reqno = this.Request.QueryString["reqno"].ToString().Trim();
            string msrno = this.Request.QueryString["msrno"].ToString().Trim();
            string supcode = this.Request.QueryString["supcode"].ToString().Trim();



            var date = this.txttodate.Text;
            string comcod = this.GetCompCode();
            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "INSERTNOADATA", strval, null, null, reqno, msrno, supcode);
            if (!result)
            {
                //
            }


        }




        private void ShowLetter()
        {

            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //string comcod = this.GetCompCode(); 
            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            //if (ds3.Tables[0].Rows.Count == 0)
            //    return;
            //string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            //this.txtml.Text = lett;

            //ViewState["letter"] = ds3.Tables[0];
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.txtml.Text = this.data(type);



        }



        protected string data(string type01)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            //created by nahid for dynamic html value
            //this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            string comaddress = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();

            string imgpge = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
            //byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(imgpge));
            //string complogo = Convert.ToBase64String(imageArray);

            string reqno = this.Request.QueryString["reqno"].ToString().Trim();
            string msrno = this.Request.QueryString["msrno"].ToString().Trim();
            string supcode = this.Request.QueryString["supcode"].ToString().Trim();

            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETNOADATAFORMAT", reqno, msrno, supcode, "", "");
            DataTable dt1 = ds3.Tables[0];


            string supname = dt1.Rows[0]["supdesc"].ToString();
            string buyerName = dt1.Rows[0]["buyerdesc"].ToString();
            string qty = dt1.Rows[0]["reqqty"].ToString();
            double amount = Convert.ToDouble(dt1.Rows[0]["ttamt"].ToString());
            string curtype = dt1.Rows[0]["curdesc"].ToString() + " ";
            string inword = ASTUtility.Trans(amount, 2);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            string tablesale = html.ToString();
            string lbody = string.Empty;
            // string empid=hst["empid"].ToString();
            string name = "Nahid";//(string)ViewState["name"];
            string Desig = "Asst. Manager";//(dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];

            string usrdesig = "Asst. Manager";//(dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["desig"].ToString();
            string usersign = "sign"; //(dt1.Rows.Count == 0) ? "" : Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            string uname = "Nahid"; //(dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empname"].ToString();

            lbody = "<div class='printHeader'><table class='tblnoa' border='0' width='800' cellspacing='0' cellpadding='0'><tbody><tr><td colspan='2'><strong>" + comnam + "</strong><br /><br /></td></tr><tr><td width='480'>Excutive Managment</td><td style = 'height: 17px;' width= '606'><strong> Note Of Approval </strong></td></tr><tr style = 'height: 131px;'><td style= 'height: 131px;' > &nbsp;</td><td style = 'height: 131px;' ><strong>" + comnam + "</strong><table class='tblnoa' border='0' width='608' cellspacing='0' cellpadding='0'><tbody><tr><td width = '139'> Name </td><td width='8'>:</td><td width = '455'> Nahid </td></tr><tr><td>Ext.No</td><td>:</td><td>5014</td></tr><tr><td>Location</td><td>:</td><td>Dhaka</td></tr><tr><td>Email</td><td>:</td><td>ad @dd.com</td></tr><tr><td>Referance</td><td>:</td><td>Shvon</td></tr><tr><td>Date</td><td>:</td><td>01-Jan-2018</td></tr></tbody></table></td></tr><tr style = 'height: 17px;'><td style= 'height: 17px;' colspan= '2'> This is for Your please give value on appropriate Box : Pre-approval post approval</td></tr></tbody></table><p><strong>Subject: <u>Confirmation of Service.</u></strong></p><p>&nbsp;</p><p>Dear<strong> Sir</strong>,</p><p>As required by our buyer <strong>" + buyerName + "</strong>, we have to purchase Sab Guard Brand Button Pull Test Machine urgently.After checking with few vendors, we find cut the required brand Machines available only in Trims Best Ltd.We have collected their price quotation and finally negotiated price is <strong>" + curtype + Convert.ToDouble(amount).ToString("#,##0.00;(#,##0.00); ") + inword + "</strong>  Including Vat &amp; A.T.</p><p>Earlier this Supplier<strong> ( " + supname + ")</strong> already Supplied <strong>" + Convert.ToDouble(qty).ToString("#,##0.00;(#,##0.00); ") + "</strong> Pcs of Color Assessment Cabinet (Light )at our footwear factory.</p><p>Thanking you</p><p>Sincerely Yours,</p><p class='pImage'>&nbsp;</p><p class='pUname'><strong>" + uname + "</strong></p><p class='pUname'><strong>" + usrdesig + "</strong></p><p>Recommendation:</p><p>&nbsp;</p><p>Md.Arman Hossan</p><p>Approval:</p><p>Executive Management</p></div>";

            return lbody;
        }





    }
}