﻿using SPEENTITY;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class SPEMaster : System.Web.UI.MasterPage
    {
        UserManager userManager = new UserManager();
        UserService objuserser = new UserService();
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["tblusrlog"] != null)
            {
                DataSet ds2 = ((DataSet)Session["tblusrlog"]);

                this.UserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
                this.UserImg2.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";

                this.lblmoduleid.Text = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                this.ShowUserPerModule();
                this.GetMenuGenerate();
            }
            else
            {
                Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");
            }

            if (Session["tbllog1"] != null)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst.Count == 0)
                {
                    Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");

                }
                this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                this.lbladd.Text = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();
                ((Image)this.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
                this.lblLoginInfo.Text = "User: " + ((DataTable)Session["tbllog1"]).Rows[0]["usrsname"].ToString() + ", Session:" + ((DataTable)Session["tbllog1"]).Rows[0]["session"].ToString(); //+ ", Login Time: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");


            }
            else
            {
                Response.Redirect("~/ErrorHandling.aspx?Type=SDestroy");
            }

        }
        private void ShowUserPerModule()
        {
            List<EClassComModule> lst = objuserser.GetComModule();
            Session["tblmodule"] = lst;
        }
        private void GetMenuGenerate()
        {


            List<EClassComModule> lst = (List<EClassComModule>)Session["tblmodule"];
            MenuItem FirstParentItem = null;

            Menu1.Items.Clear();

            foreach (EClassComModule lst1 in lst)
            {
                FirstParentItem = new MenuItem(lst1.modulenam);
                FirstParentItem.Value = lst1.moduleid;
                // FirstParentItem.Selectable = slct;
                FirstParentItem.NavigateUrl = lst1.url;
                Menu1.Items.Add(FirstParentItem);

            }




        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];
            //string str =ASTUtility.Left((hst["comcod"].ToString()),1);
            return ASTUtility.Left((hst["comcod"].ToString()), 1);
        }

        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnHisprice_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnTranList_Click(object sender, EventArgs e)
        {

        }
        protected void chkBoxN_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnAdd_Click1(object sender, EventArgs e)
        {

        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            //if (this.lnkbtnEdit_Click(null,null)==true)
            //{
            //    this.lblmsg.Visible = true;
            //    this.lblmsg.Text = "Please select your item for edit";
            //}

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}