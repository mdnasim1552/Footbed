﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class HrWinMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

        }
        protected void lnkbtnHRM_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "80";
            ds1.Tables[0].Rows[0]["moduleid2"] = "80";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;


            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('DeafultMenu.aspx?Type=7000', target='_self');</script>";
        }


    }
}