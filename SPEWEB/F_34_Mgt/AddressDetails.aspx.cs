using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_34_Mgt
{
    public partial class AddressDetails : System.Web.UI.Page
    {
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtAddPoCod.Visible = false;
                // string comcod = this.GetCompCode();
                this.txtAddPo.Visible = false;
                this.txtAddPoCod.Visible = false;
                this.txtAddPoBN.Visible = false;
                this.lblAddPo.Visible = false;
                this.lblAddPoBN.Visible = false;
                this.lblAddPoCod.Visible = false;
                this.lnkNew.Text = "new";
                ((Label)this.Master.FindControl("lblTitle")).Text = "Address Basic Information";
                //getDdlData();

                this.GetCouDivDistUzPo();
            }
        }

        private void GetCouDivDistUzPo()
        {
            DataSet ds = da.GetTransInfo("", "SP_ENTRY_COMN_INFO_MGT", "GETCOUNTRYDIVDISTUPZPO", "", "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                return;
            }
            
            this.ddldivision.DataTextField = "name";
            this.ddldivision.DataValueField = "id";
            this.ddldivision.DataSource = ds.Tables[1];
            this.ddldivision.DataBind();
            ddldivision_SelectedIndexChanged(null,null);
           
        }
           
        protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDistrict();

            //this.frm_dis.Visible = true;
            //this.AddDv.Visible = false;
            ddldist_SelectedIndexChanged(null, null);
            VisiblePart();
        }

        private void GetDistrict()
        {
            string divid = this.ddldivision.SelectedValue.ToString();

            DataSet ds = da.GetTransInfo("", "SP_ENTRY_COMN_INFO_MGT", "GETCOUNTRYDIVDISTUPZPO", divid, "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                return;
            }
            this.ddldist.DataTextField = "engdist";
            this.ddldist.DataValueField = "distid";
            this.ddldist.DataSource = ds.Tables[2];
            this.ddldist.DataBind();
        }

        protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUpozila();

            //this.frm_dis.Visible = true;
            //this.frm_upz.Visible = true;
            //this.AddDv.Visible = false;
            //this.AddDist.Visible = false;
           
            VisiblePart();
        }

        private void GetUpozila()
        {
            string id = this.ddldist.SelectedValue.ToString();

            DataSet ds = da.GetTransInfo("", "SP_ENTRY_COMN_INFO_MGT", "GETCOUNTRYDIVDISTUPZPO", id, "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                return;
            }

            this.ddlupzila.DataTextField = "engupz";
            this.ddlupzila.DataValueField = "upzid";
            this.ddlupzila.DataSource = ds.Tables[3];
            this.ddlupzila.DataBind();
        }

        protected void ddlupzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPostOffice();
            VisiblePart();
        }

        private void GetPostOffice()
        {
            string id = this.ddlupzila.SelectedValue.ToString();

            DataSet ds = da.GetTransInfo("", "SP_ENTRY_COMN_INFO_MGT", "GETCOUNTRYDIVDISTUPZPO", id, "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                return;
            }

            this.ddlpost.DataTextField = "engpost";
            this.ddlpost.DataValueField = "postid";
            this.ddlpost.DataSource = ds.Tables[4];
            this.ddlpost.DataBind();

            ddlpost_SelectedIndexChanged(null, null);
        }

        protected void ddlpost_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = this.ddlpost.SelectedValue.ToString();
            if (id != "0")
            {
                this.GetVillageinfo();

            }
        }

        private void GetVillageinfo()
        {
            string id = this.ddlpost.SelectedValue.ToString();

            DataSet ds = da.GetTransInfo("", "SP_ENTRY_COMN_INFO_MGT", "GETVILLAGEINFO", id, "", "", "", "", "", "", "", "");

            if (ds == null)
            {
                return;
            }
            this.GvVillInf0.DataSource = ds.Tables[0];
            this.GvVillInf0.DataBind();

          //  VisiblePart();

        }

        private void VisiblePart()
        {
            string division = this.ddldivision.SelectedValue.ToString();
            string dist = this.ddldist.SelectedValue.ToString() == "" ? "0" : this.ddldist.SelectedValue.ToString();
            string upzila = this.ddlupzila.SelectedValue.ToString() == "" ? "0" : this.ddlupzila.SelectedValue.ToString();
            string post = this.ddlpost.SelectedValue.ToString() == "" ? "0" : this.ddlpost.SelectedValue.ToString();
            string nameeng = this.TxtVillageEng.Text.ToString();
            string nameban = this.TxtVillBan.Text.ToString();
            string code = this.txtCode.Text.ToString();

            bool result;
            if (division == "0" && dist == "0" && upzila == "0" && post == "0")
            {
             
                this.frm_dis.Visible = false;
                this.frm_upz.Visible = false;
                this.frm_Post.Visible = false;
            }
            else if (division != "0" && dist == "0" && upzila == "0" && post == "0")
            {
                this.AddDv.Visible = false;
                this.AddDist.Visible = true;
                this.frm_dis.Visible = true;
                this.frm_upz.Visible = false;
                this.frm_Post.Visible = false;
            }
            else if (division != "0" && dist != "0" && upzila == "0" && post == "0")
            {
                this.AddDv.Visible = false;
                this.AddDist.Visible = false;
                this.AddUpz.Visible = true;
                this.frm_dis.Visible = true;
                this.frm_upz.Visible = true;
                this.frm_Post.Visible = false;
            }
            else if (division != "0" && dist != "0" && upzila != "0" && post == "0")
            {
                this.AddDv.Visible = false;
                this.AddDist.Visible = false;
                this.AddUpz.Visible = false;
                this.frm_dis.Visible = true;
                this.frm_upz.Visible = true;
                this.frm_Post.Visible = true;
            }
            else if (division != "0" && dist != "0" && upzila != "0" && post != "0")
            {
                this.AddDv.Visible = false;
                this.AddDist.Visible = false;
                this.AddUpz.Visible = false;
                this.AddPost.Visible = false;
                this.frm_dis.Visible = true;
                this.frm_upz.Visible = true;
                this.frm_Post.Visible = true;

            }



        }


        protected void addDiv_Click(object sender, EventArgs e)
        {
          
            this.pnlFrom.Visible = true;
        }

        protected void lnkAdDist_Click(object sender, EventArgs e)
        {
            this.addDiv.Visible = false;
            this.pnlFrom.Visible = true;
        }

        protected void lnkAdUpz_Click(object sender, EventArgs e)
        {
            this.addDiv.Visible = false;
            this.AddDist.Visible = false;
            this.pnlFrom.Visible = true;
        }

        protected void lnkAdPost_Click(object sender, EventArgs e)
        {
            this.addDiv.Visible = false;
            this.AddDist.Visible = false;
            this.AddUpz.Visible = false;
            this.pnlFrom.Visible = true;
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            string division = this.ddldivision.SelectedValue.ToString();
            string dist = this.ddldist.SelectedValue.ToString() == "" ? "0" : this.ddldist.SelectedValue.ToString();
            string upzila = this.ddlupzila.SelectedValue.ToString() == "" ? "0" : this.ddlupzila.SelectedValue.ToString();
            string post = this.ddlpost.SelectedValue.ToString() == "" ? "0" : this.ddlpost.SelectedValue.ToString();
            string nameeng = this.TxtVillageEng.Text.ToString();

            if (nameeng == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Please enter name(eng)');", true);
                return;
            }
            string nameban = this.TxtVillBan.Text.ToString();
            string code = this.txtCode.Text.ToString();

            bool result;
            if (division == "0" && dist == "0" && upzila == "0" && post == "0")
            {

                result = this.da.UpdateTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTADDRESSINFO", division, "0", "0", "0", nameeng, nameban, "");
                if (!result)
                {

                    //return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Division Inserted Successfully');", true);
                    this.GetCouDivDistUzPo();
                   
                }
            }
            else if (division != "0" && dist == "0" && upzila == "0" && post == "0")
            {
                result = this.da.UpdateTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTADDRESSINFO", division, "0", "0", "0", nameeng, nameban, "");
                if (!result)
                {

                    //return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('District Successfully Inserted');", true);

                    this.GetDistrict();
                    
                }
            }
            else if (division != "0" && dist != "0" && upzila == "0" && post == "0")
            {
                result = this.da.UpdateTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTADDRESSINFO", division, dist, "0", "0", nameeng, nameban, "");
                if (!result)
                {

                    //return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Upazila Inserted Successfully');", true);
                    // this.GetCouDivDistUzPo();
                    
                }
            }
            else if (division != "0" && dist != "0" && upzila != "0" && post == "0")
            {
                result = this.da.UpdateTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTADDRESSINFO", division, dist, upzila, "0", nameeng, nameban, "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Insert Failed');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Post Office Inserted Successfully');", true);
                    this.GetPostOffice();
                   
                }
                //ddlupzila_SelectedIndexChanged(null,null);
            }
            else if (division != "0" && dist != "0" && upzila != "0" && post != "0")
            {
                result = this.da.UpdateTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTADDRESSINFO", division, dist, upzila, post, nameeng, nameban, "");
                if (!result)
                {

                    //return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Village Inserted Successfully');", true);
                    GetVillageinfo();
                }
            }

            this.Reffessfield();
        }

        private void Reffessfield()
        {
            this.TxtVillageEng.Text = "";
            this.TxtVillBan.Text = "";
            this.txtCode.Text = "";
        }


        // Old Work 
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkNew.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    ViewState.Remove("storedata");

                    //this.ddlpagesize.Enabled = true;
                    string Code = (this.ddlDistLst.SelectedValue.ToString()).Substring(0, 2);

                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                catch (Exception ex)
                {
                    this.ConfirmMessage.Text = "Information not found!!!!";
                }
            }
            else if (this.lnkNew.Text == "new")
            {
                this.txtAddPo.Text = "";
                this.txtAddPoCod.Text = "";
                this.txtAddPoBN.Text = "";
                this.txtAddPo.Visible = true;
                this.txtAddPoCod.Visible = true;
                this.txtAddPoBN.Visible = true;
                this.lblAddPo.Visible = true;
                this.lblAddPoBN.Visible = true;
                this.lblAddPoCod.Visible = true;
                this.lnkNew.Text = "Save";
            }
            else
            {
                //this.lnkok.Text = "Ok";
                if (this.txtAddPo.Text.ToString() != "" & this.txtAddPoBN.Text.ToString() != "" & this.txtAddPoCod.Text.ToString() != "")
                {
                    string txtAddPo = this.txtAddPo.Text.ToString();
                    string txtAddBN = this.txtAddPoBN.Text.ToString();
                    string txtAddPoCod = this.txtAddPoCod.Text.ToString();

                    var UpId = this.ddlUpziLst.SelectedValue.ToString();
                    //this.ddlDistLst.Enabled = true;
                    ////this.ddlOthersBookSegment.Enabled = true;
                    //this.grvacc.DataSource = null;
                    //this.grvacc.DataBind();
                    if (UpId != "0")
                    {
                        DataSet ds1 = this.da.GetTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "INSERTPO", "", UpId, txtAddPo, txtAddBN, txtAddPoCod);
                    }

                    this.txtAddPo.Text = "";
                    this.txtAddPoCod.Text = "";
                    this.txtAddPoBN.Text = "";
                    this.txtAddPo.Visible = false;
                    this.txtAddPoCod.Visible = false;
                    this.txtAddPoBN.Visible = false;
                    this.lblAddPo.Visible = false;
                    this.lblAddPoBN.Visible = false;
                    this.lblAddPoCod.Visible = false;
                    this.lnkNew.Text = "new";
                    All_DataBind();
                    this.grvacc_DataBind();
                }
                else
                {

                    this.txtAddPo.Text = "";
                    this.txtAddPoCod.Text = "";
                    this.txtAddPoBN.Text = "";
                    this.txtAddPo.Visible = false;
                    this.txtAddPoCod.Visible = false;
                    this.txtAddPoBN.Visible = false;
                    this.lblAddPo.Visible = false;
                    this.lblAddPoBN.Visible = false;
                    this.lblAddPoCod.Visible = false;
                    this.lnkNew.Text = "new";
                    All_DataBind();
                    this.grvacc_DataBind();
                }

            }
        }
        protected void ddlDistLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tempddl1 = (this.ddlDistLst.SelectedValue.ToString());
            DataSet ds = this.da.GetTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "GETUPZILIST", tempddl1);
            ViewState["storeddlupzidata"] = ds.Tables[0];

            this.ddlUpziLst_DataBind();
            All_DataBind();
            this.grvacc_DataBind();
        }
        protected void ddlUpziLst_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataView dv = new DataView((DataTable)ViewState["storeddlAlldata"]);
            string tempddl2 = this.ddlUpziLst.SelectedValue.ToString().Trim();
            dv.RowFilter = "upziNameId= '" + tempddl2 + "'";

            DataTable dt1 = dv.ToTable();
            if (tempddl2 == "0")
            {
                ViewState["storeddlFltrdata"] = (DataTable)ViewState["storeddlAlldata"];
            }
            else
            {
                ViewState["storeddlFltrdata"] = dt1;
            }

            this.grvacc_DataBind();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void grvacc_DataBind()
        {
            try
            {

                DataTable dt = (DataTable)ViewState["storeddlFltrdata"];
                this.grvacc.DataSource = dt;
                this.grvacc.DataBind();
            }
            catch (Exception ex)
            {
            }

        }
        protected void ddlDistLst_DataBind()
        {
            try
            {
                DataTable ds1 = (DataTable)ViewState["storeddldistdata"];

                this.ddldist.DataTextField = "distName";
                this.ddldist.DataValueField = "Id";
                this.ddldist.DataSource = ds1;
                this.ddldist.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        protected void ddlUpziLst_DataBind()
        {
            try
            {

                DataTable ds2 = (DataTable)ViewState["storeddlupzidata"];

                this.ddlupzila.DataTextField = "UpName";
                this.ddlupzila.DataValueField = "Id";
                this.ddlupzila.DataSource = ds2;
                this.ddlupzila.DataBind();

                All_DataBind();
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }

        }

        protected void All_DataBind()
        {
            try
            {
                string tempddl1 = (this.ddlDistLst.SelectedValue.ToString());
                string tempddl2 = this.ddlUpziLst.SelectedValue.ToString().Trim();
                DataSet ds = this.da.GetTransInfo(GetCompCode(), "SP_ENTRY_COMN_INFO_MGT", "GETAllLIST", tempddl1, tempddl2);
                ViewState["storeddlAlldata"] = ds.Tables[0];
                ViewState["storeddlFltrdata"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }

        }
        private void getDdlData()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlDistLst.SelectedValue.ToString());
            string tempddl2 = this.ddlUpziLst.SelectedValue.ToString().Trim();
            //string tempddl3 = this.ddlPostOff.SelectedValue.ToString().Trim();
            //string qtype = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_COMN_INFO_MGT", "GETDISLIST");
            DataSet ds2 = this.da.GetTransInfo(comcod, "SP_ENTRY_COMN_INFO_MGT", "GETUPZILIST");

            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                //this.lnknewentry.Visible = true;

            }


            ViewState["storeddldistdata"] = ds1.Tables[0];
            ViewState["storeddlupzidata"] = ds2.Tables[0];

            ddlDistLst_DataBind();
            ddlUpziLst_DataBind();

        }
        private void ShowInformation()
        {
            //string comcod = this.GetCompCode();
            //string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //string qtype = this.Request.QueryString["Type"].ToString();

            //DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_SALE_CODE_DETAILS", tempddl1,
            //                tempddl2, qtype, "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //{
            //    //this.lnknewentry.Visible = true;

            //}
            //ViewState["storedata"] = ds1.Tables[0];
        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable ds2 = (DataTable)ViewState["storeddlFltrdata"];
            int Index = e.RowIndex;
            string upzilla_id = ds2.Rows[Index]["upziNameid"].ToString();
            string id = ds2.Rows[Index]["poNameId"].ToString();

            this.da.GetTransInfo(comcod, "SP_ENTRY_COMN_INFO_MGT", "DELPOST", id, upzilla_id);
            ddlDistLst_SelectedIndexChanged(null, null);
        }

    }
}