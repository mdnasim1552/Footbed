using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using SPELIB;
using SPERDLC;
using SPEENTITY;
using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_15_DPayReg
{
    public partial class LinkQutaAttached : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Qutation Upload Process";//

            }
        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }

        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();

            string reqno = this.Request.QueryString["reqno"].ToString(); //ASTUtility.Left(this.lblCurreqno1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurreqno1.Text.Trim().Substring(3, 2) + this.txtCurreqno2.Text.Trim();

            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            // i = i + 1;


            if (AsyncFileUpload1.HasFile)
            {


                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/") + reqno + extension);

                Url = reqno + extension;

            }
            //Url = Url.Substring(0,(Url.Length-1));

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGEUPLOADOT", reqno, Url, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {


                //this.lblMesg.Text=" Successfully Updated ";
            }

        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblimgPath");

            DataTable tbfilePath = new DataTable();
            tbfilePath.Columns.Add("filePath1", Type.GetType("System.String"));
            tbfilePath.Columns.Add("supinfo", Type.GetType("System.String"));

            ViewState["tblimgPath"] = tbfilePath;

            DataTable tbl2 = (DataTable)ViewState["tblimgPath"];
            string comcod = this.GetCompCode();
            // string ssircode = this.ddlBestSupplier.SelectedValue.ToString();

            string reqno = this.Request.QueryString["reqno"].ToString();//ASTUtility.Left(this.lblCurreqno1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurreqno1.Text.Trim().Substring(3, 2) + this.txtCurreqno2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGESHOWOT", reqno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];

            string Url = "";
            string supinfo = "";
            for (int j = 0; j < tbl1.Rows.Count; j++)
            {

                Url = "../Uploads/" + tbl1.Rows[j]["attacheddoc"].ToString().Trim();
                supinfo = tbl1.Rows[j]["SIRDESC"].ToString().Trim();
                DataRow dr1 = tbl2.NewRow();
                dr1["filePath1"] = Url;
                dr1["supinfo"] = supinfo;
                tbl2.Rows.Add(dr1);

            }




            ListViewEmpAll.DataSource = tbl2;
            ListViewEmpAll.DataBind();
            ViewState["tblimgPath"] = tbl2;


        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
            //    HtmlGenericControl divControl = e.Item.FindControl("EmpAll") as HtmlGenericControl;
            //    DataRowView dataRow = ((DataRowView)listViewDataItem.DataItem);
            //    divControl.Attributes.Add("ID", dataRow["idcardno"].ToString());


            //}

        }
    }
}