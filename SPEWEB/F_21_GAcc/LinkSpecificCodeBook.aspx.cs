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
using SPEENTITY;
using AjaxControlToolkit;
using System.IO;
using System.Drawing.Drawing2D;
using Image = System.Drawing.Image;
using System.Drawing;

namespace SPEWEB.F_21_GAcc
{
    public partial class LinkSpecificCodeBook : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        UserManager objUser = new UserManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Specification Code";
                lblMaterialName.Text = this.Request.QueryString["sirdesc"].ToString();
                this.ShowInformation();
            }

            if (fileuploaddropzone.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                Upload = System.IO.Path.GetFileName(fileuploaddropzone.PostedFile.FileName);
                string extension = Path.GetExtension(fileuploaddropzone.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();

                string Url = "~/Upload/Material/" + random + extension;
                string savelocation = Server.MapPath("~") + "\\Upload/Material" + "\\" + random + extension;


                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    //string filePath = Server.MapPath("~/Upload/Material/") + fileuploaddropzone.FileName;
                    //fileuploaddropzone.SaveAs(Url);

                    fileuploaddropzone.PostedFile.SaveAs(savelocation);
                    FileInfo fileInfo = new FileInfo(savelocation);
                    long fileSize = fileInfo.Length;

                    if (fileSize > 102400)
                    {
                        ResizeImage(savelocation, 220);
                    }
                }

                string filepath = savelocation;

               // fileuploaddropzone.PostedFile.SaveAs(savelocation);

                string sircode = this.LblSirSpcfcod.Text.Substring(0,12);
                string spcfcod = this.LblSirSpcfcod.Text.Substring(12, 12);
                string comcod = this.GetComeCode();

                bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTIMAGEUP", sircode, spcfcod, Url, "", "", "", "", "", "");

                if(result)
                {
                    this.ShowInformation();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Image Updated Successfully.');", true);
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Image Upload Failed.');", false);
                   
                }
            }

           
        }


        private void ResizeImage(string Url, int targetSize)
        {
            using (Image image = Image.FromFile(Url))
            {
                int newWidth, newHeight;

                if (image.Width > image.Height)
                {
                    newWidth = targetSize;
                    newHeight = (int)(image.Height * (targetSize / (float)image.Width));
                }
                else
                {
                    newHeight = targetSize;
                    newWidth = (int)(image.Width * (targetSize / (float)image.Height));
                }

                using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                    image.Dispose();
                    File.Delete(Url);
                    newImage.Save(Url);
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();    
        }

        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                this.grvacc.EditIndex = e.NewEditIndex;
                this.grvacc_DataBind();

                string comcod = this.GetComeCode();

                //DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");

                //DropDownList ddlgval = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlunit");
                //ddlgval.DataTextField = "gdesc";
                //ddlgval.DataValueField = "gcod";
                //ddlgval.DataSource = dsone.Tables[0];
                //ddlgval.DataBind();


                //DropDownList ddlgvinco = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlgvinco");
                //DataRow dr1;

                //DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "17%", "", "", "", "", "");
                //dr1 = ds1.Tables[0].NewRow();
                //dr1["gcod"] = "00000";
                //dr1["gdesc"] = "NONE";
                //dr1["comcod"] = comcod;
                //ds1.Tables[0].Rows.Add(dr1);
                //ddlgvinco.DataValueField = "gcod";
                //ddlgvinco.DataTextField = "gdesc";
                //ddlgvinco.DataSource = ds1.Tables[0];
                //ddlgvinco.DataBind();
            }
            catch (Exception ex)
            {
                return;
            }


        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                this.lblmsg.Visible = true;
                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
                string sircode = "";
                if (sircode1.Length == 5)
                {
                    sircode = sircode2 + sircode1;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid code!');", false);
                    return;

                }
                if (ASTUtility.Right(sircode, 3) == "000")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Update the Detail Code.');", true);
                    return;
                }
                string Impacod = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvimpa")).Text.Trim();
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string Desc1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc1")).Text.Trim();
                string Desc2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc2")).Text.Trim();
                string Desc3 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc3")).Text.Trim();
                string Desc4 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc4")).Text.Trim();
                string sirval = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim();
                

                //string allowance = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvAllowance")).Text.Trim();
                //string unitcode = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.Value;
                //string unitdesc = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.Text;
                //string incotrmcode = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlgvinco")).SelectedItem.Value;
                //string incotrmdesc = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlgvinco")).SelectedItem.Text;
                //string cfprcnt = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvCfPercnt")).Text.Trim();


                string rsircode = this.Request.QueryString["sircode"].ToString();
                int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                this.grvacc.EditIndex = -1;
                bool result = false;
                string allwance = "0";

                result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, Impacod, rsircode,
                    Desc1, Desc2, Desc3, Desc4, sirval, allwance, "", "", "", "", "", "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully.');", true);

                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Specification CodeBook";
                        string eventdesc = "Update CodeBook";
                        string eventdesc2 = sircode;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Parent Code Not Found !!!');", false);
                }
                this.ShowInformation();
            }

            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                List<SPEENTITY.C_21_Acc.EClassSpecification> lst = (List<SPEENTITY.C_21_Acc.EClassSpecification>)Session["storedata"];

                string comcod = this.GetComCode();

                switch (comcod)
                {
                    case "5305":
                    case "5306":

                        if (Request.QueryString["sircode"].ToString().Substring(0, 2) == "22")
                        {
                            this.grvacc.Columns[4].HeaderText = "Machine Name / Parts No";
                            
                        }
                        else
                        {
                            this.grvacc.Columns[4].HeaderText = "Code";
                            this.grvacc.Columns[11].Visible = false;
                            this.grvacc.Columns[12].Visible = false;

                        }

                        this.grvacc.DataSource = lst;
                        this.grvacc.DataBind();

                        TextBox txtboxSpcf = (TextBox)this.grvacc.HeaderRow.FindControl("txtSrchThickns");
                        TextBox txtboxLxw = (TextBox)this.grvacc.HeaderRow.FindControl("txtSrchLxw");

                        if (Request.QueryString["sircode"].ToString().Substring(0, 2) == "22")
                        {
                            txtboxSpcf.Attributes.Add("placeholder", "Specification");
                            txtboxLxw.Attributes.Add("placeholder", "Origin");
                        }
                        else
                        {
                            txtboxSpcf.Attributes.Add("placeholder", "Thickness");
                            txtboxLxw.Attributes.Add("placeholder", "Lxw");
                        }

                        break;

                    default:
                        this.grvacc.DataSource = lst;
                        this.grvacc.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void ShowInformation()
        {

            string comcod = this.GetComeCode();
            Session.Remove("storedata");
            string spcfcod = this.Request.QueryString["sircode"].ToString();
            List<SPEENTITY.C_21_Acc.EClassSpecification> lst = new List<SPEENTITY.C_21_Acc.EClassSpecification>();

            lst = objUser.ShowSpecification("SP_ENTRY_CODEBOOK", "SPACCOUNTINFO02", spcfcod);
            //DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTINFO02", spcfcod,
            //                      "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.grvacc.DataSource = null;
            //    this.grvacc.DataBind();
            //    return;
            //}

            Session["storedata"] = lst;
            this.grvacc_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTSPECIFICATIONCODE", "", "");
            //ReportDocument rptstk = new MFGRPT.R_17_Acc.RptSpecification() ;
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text =comadd;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Specification CodeBook";
            //    string eventdesc = "Print CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptstk.SetDataSource(ds1.Tables[0]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ibtnSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowInformation();
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgvdesc");
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString();


                if (Code == "")
                    return;
                if (ASTUtility.Right(Code, 3) != "000")
                {
                    //string rsircode = this.Request.QueryString["sircode"].ToString();
                    //hlnkgvdesc.Style.Add("color", "blue");
                    //string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfdesc")).ToString();
                    //hlnkgvdesc.NavigateUrl = "~/F_14_Pro/LinkPurSurveryLink.aspx?sircode=" + rsircode + "&sirdesc=" + sirdesc;
                }

            }

        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            string sircode = this.Request.QueryString["sircode"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;         
            string sircode1 = ((Label)grvacc.Rows[index].FindControl("lbgrcod")).Text.Trim().Replace("-", "");
            string sircode2 = ((Label)grvacc.Rows[index].FindControl("lblspccod2")).Text.Trim().Replace("-", "");
            string spcfcod = "";
            if (sircode1.Length == 5)
            {
                spcfcod = sircode2 + sircode1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid code!');", false);
                return;

            }
            if (ASTUtility.Right(sircode1, 3) == "000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Update the Detail Code.');", true);
                return;
            }
            this.LblSirSpcfcod.Text = sircode + spcfcod;
            this.LblSirSpcfcod.Visible = false;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

    }
}