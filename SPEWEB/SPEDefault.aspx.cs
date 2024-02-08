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
    public partial class SPEDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tblusrlog"] != null)
            {
                if (IsPostBack)
                    return;
                string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                string ModuleID2 = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();
                string comcod = ASTUtility.Left(((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim(), 1);
                // this.ddlModuleName.Visible = (ModuleID == "AA");
                if (ModuleID == "AA" && comcod == "4")
                    this.ddlModuleName.SelectedValue = "81";

                if (ModuleID == "AA" && comcod == "8")
                    this.ddlModuleName.SelectedValue = "36";
                if (ModuleID2 != "AA")
                    this.ddlModuleName.SelectedValue = ModuleID2;

                this.GetCompanyName();
                this.ddlModuleName_SelectedIndexChanged(null, null);


            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetCompanyName()
        {

            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();


            //string name = "";
            //DataTable dt = ds1.Tables[0];


            //for (int i=0;i<dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["compcod"].ToString() != "")
            //        name = name + dt.Rows[i]["compcod"].ToString() + ", ";


            //}

            //name =(name.Length)>0?ASTUtility.Left(name, (name.Length - 2)):name;
            //if (name.Length > 0)
            //{
            //    DataView dv = ds1.Tables[0].DefaultView;
            //    dv.RowFilter = ("comcod not in (" + name + ")");
            //    dt = dv.ToTable();
            //}


            this.ddlCompanyName.DataTextField = "comsnam";
            this.ddlCompanyName.DataValueField = "comcod";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName.SelectedValue = this.GetCompCode();
            this.ModuleVisible();


        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ModuleVisible();
            string comcod = this.ddlCompanyName.SelectedValue.ToString().Substring(0, 1);
            if (comcod == "5")
                this.ddlModuleName.SelectedValue = "01";

            else if (comcod == "4")
                this.ddlModuleName.SelectedValue = "81";

            else if (comcod == "8")
                this.ddlModuleName.SelectedValue = "36";



            this.GetUserPermission();
            this.MasComNameAndAdd();
            this.ddlModuleName_SelectedIndexChanged(null, null);


        }



        private void MasComNameAndAdd()
        {
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = this.ddlCompanyName.SelectedItem.Text;// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            ((Label)this.Master.FindControl("lbladd")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();




        }
        private void GetUserPermission()
        {
            ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompanyName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess("ASITHRM") : new ProcessAccess();
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = this.ddlModuleName.SelectedValue.ToString();
            string modulename = this.ddlModuleName.SelectedItem.Text.Trim();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;
            string Comcode = this.ddlCompanyName.SelectedValue.ToString();
            string ComName = this.ddlCompanyName.SelectedItem.ToString();

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = Comcode;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;



        }
        private void ModuleVisible()
        {


            //string CompName = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim();
            //string CompType = ASTUtility.Left(CompName, 1);

            string CompName = this.ddlCompanyName.SelectedValue.ToString();
            string CompType = ASTUtility.Left(CompName, 1);


            this.ddlModuleName.Items[0].Enabled = (CompType == "5");
            this.ddlModuleName.Items[1].Enabled = (CompType == "5");
            this.ddlModuleName.Items[2].Enabled = (CompType == "5");
            this.ddlModuleName.Items[3].Enabled = (CompType == "5");
            this.ddlModuleName.Items[4].Enabled = (CompType == "5");
            this.ddlModuleName.Items[5].Enabled = (CompType == "5");
            this.ddlModuleName.Items[6].Enabled = (CompType == "5");
            this.ddlModuleName.Items[7].Enabled = (CompType == "5");
            this.ddlModuleName.Items[8].Enabled = (CompType == "5");
            this.ddlModuleName.Items[9].Enabled = (CompType == "5");
            this.ddlModuleName.Items[10].Enabled = (CompType == "5");
            this.ddlModuleName.Items[11].Enabled = (CompType == "5");
            this.ddlModuleName.Items[12].Enabled = (CompType == "5");
            this.ddlModuleName.Items[13].Enabled = (CompType == "5");
            this.ddlModuleName.Items[14].Enabled = (CompType == "5");
            this.ddlModuleName.Items[15].Enabled = (CompType == "5");
            this.ddlModuleName.Items[16].Enabled = (CompType == "5");
            this.ddlModuleName.Items[17].Enabled = (CompType == "5");
            this.ddlModuleName.Items[18].Enabled = (CompType == "5");
            this.ddlModuleName.Items[19].Enabled = (CompType == "5");
            this.ddlModuleName.Items[20].Enabled = (CompType == "5");
            this.ddlModuleName.Items[21].Enabled = (CompType == "5");
            this.ddlModuleName.Items[22].Enabled = (CompType == "5");
            this.ddlModuleName.Items[23].Enabled = (CompType == "5");

            this.ddlModuleName.Items[24].Enabled = (CompType == "8");
            this.ddlModuleName.Items[25].Enabled = (CompType == "8");

            this.ddlModuleName.Items[26].Enabled = (CompType == "4");
            this.ddlModuleName.Items[27].Enabled = (CompType == "4");
            this.ddlModuleName.Items[28].Enabled = (CompType == "4");
            this.ddlModuleName.Items[29].Enabled = (CompType == "4");
            this.ddlModuleName.Items[30].Enabled = (CompType == "4");
            this.ddlModuleName.Items[31].Enabled = (CompType == "4");
            this.ddlModuleName.Items[32].Enabled = (CompType == "4");
            this.ddlModuleName.Items[33].Enabled = (CompType == "4");
            this.ddlModuleName.Items[34].Enabled = (CompType == "4");
            this.ddlModuleName.Items[35].Enabled = (CompType == "4");
            this.ddlModuleName.Items[36].Enabled = (CompType == "4");
            this.ddlModuleName.Items[37].Enabled = (CompType == "4");
            this.ddlModuleName.Items[38].Enabled = (CompType == "4");
            this.ddlModuleName.Items[39].Enabled = (CompType == "4");








        }


        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(ModuleID=="AA" &&  comcod=="4")
            //    this.ddlModuleName.SelectedValue = "81";

            //if (ModuleID == "AA" && comcod == "9")
            //    this.ddlModuleName.SelectedValue = "41";
            //if (ModuleID2 != "AA")
            //    this.ddlModuleName.SelectedValue = ModuleID2;
            string ModuleID = this.ddlModuleName.SelectedValue.ToString().Trim();
            this.LblTableOfContent.Text = "Table of Content - " + this.ddlModuleName.SelectedItem.Text;
            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["ModuleID2"] = ModuleID;
            ds1.Tables[0].Rows[0]["modulename"] = this.ddlModuleName.SelectedItem.Text;

            Session["tblusrlog"] = ds1;
            this.PopulateDesktopLinks(ModuleID);
        }
        protected void PopulateDesktopLinks(string ModuleID)
        {
            DataTable tbl1 = ConstantInfo.MenuTable(ModuleID);
            Label[] lbl1 = { lblANo, lblBNo, lblCNo };
            string comcod = this.GetCompCode();
            for (int i = 2; i <= 4; i++)
            {
                DataView dv1 = tbl1.DefaultView;
                dv1.RowFilter = "itemcod like '0" + i.ToString() + "%'";
                DataTable tbl2 = dv1.ToTable();
                Image[] lbli = { hlnkA00i, hlnkA01i, hlnkA02i, hlnkA03i, hlnkA04i, hlnkA05i, hlnkA06i, hlnkA07i, hlnkA08i, hlnkA09i, hlnkA10i, hlnkA11i, hlnkA12i, hlnkA13i,
                               hlnkA14i, hlnkA15i, hlnkA16i, hlnkA17i, hlnkA18i, hlnkA19i, hlnkA20i, hlnkA21i, hlnkA22i, hlnkA23i, hlnkA24i, hlnkA25i, hlnkA26i, hlnkA27i,
                               hlnkA28i, hlnkA29i, hlnkA30i, hlnkA31i, hlnkA32i, hlnkA33i, hlnkA34i, hlnkA35i, hlnkA36i, hlnkA37i, hlnkA38i, hlnkA39i, hlnkA40i, hlnkA41i,
                               hlnkA42i, hlnkA43i, hlnkA44i, hlnkA45i, hlnkA46i, hlnkA47i, hlnkA48i, hlnkA49i, hlnkA50i, hlnkA51i,
                               hlnkA52i, hlnkA53i, hlnkA54i, hlnkA55i, hlnkA56i, hlnkA57i, hlnkA58i, hlnkA59i, hlnkA60i };

                HyperLink[] hlnk1 = { hlnkA00, hlnkA01, hlnkA02, hlnkA03, hlnkA04, hlnkA05, hlnkA06, hlnkA07, hlnkA08, hlnkA09, hlnkA10, hlnkA11, hlnkA12, hlnkA13, hlnkA14,
                                    hlnkA15, hlnkA16, hlnkA17, hlnkA18, hlnkA19, hlnkA20, hlnkA21, hlnkA22, hlnkA23, hlnkA24, hlnkA25, hlnkA26, hlnkA27, hlnkA28, hlnkA29,
                                    hlnkA30, hlnkA31, hlnkA32, hlnkA33, hlnkA34, hlnkA35, hlnkA36, hlnkA37, hlnkA38, hlnkA39, hlnkA40, hlnkA41, hlnkA42, hlnkA43, hlnkA44,
                                 hlnkA45, hlnkA46, hlnkA47, hlnkA48, hlnkA49, hlnkA50, hlnkA51, hlnkA52, hlnkA53, hlnkA54, hlnkA55, hlnkA56, hlnkA57, hlnkA58, hlnkA59, hlnkA60};
                if (i == 3)
                {
                    hlnk1[0] = hlnkB00; hlnk1[1] = hlnkB01; hlnk1[2] = hlnkB02; hlnk1[3] = hlnkB03; hlnk1[4] = hlnkB04;
                    hlnk1[5] = hlnkB05; hlnk1[6] = hlnkB06; hlnk1[7] = hlnkB07; hlnk1[8] = hlnkB08; hlnk1[9] = hlnkB09;
                    hlnk1[10] = hlnkB10; hlnk1[11] = hlnkB11; hlnk1[12] = hlnkB12; hlnk1[13] = hlnkB13; hlnk1[14] = hlnkB14; hlnk1[15] = hlnkB15;
                    hlnk1[16] = hlnkB16; hlnk1[17] = hlnkB17; hlnk1[18] = hlnkB18; hlnk1[19] = hlnkB19; hlnk1[20] = hlnkB20;
                    hlnk1[21] = hlnkB21; hlnk1[22] = hlnkB22; hlnk1[23] = hlnkB23; hlnk1[24] = hlnkB24; hlnk1[25] = hlnkB25;
                    hlnk1[26] = hlnkB26; hlnk1[27] = hlnkB27; hlnk1[28] = hlnkB28; hlnk1[29] = hlnkB29; hlnk1[30] = hlnkB30;
                    hlnk1[31] = hlnkB31; hlnk1[32] = hlnkB32; hlnk1[33] = hlnkB33; hlnk1[34] = hlnkB34; hlnk1[35] = hlnkB35;
                    hlnk1[36] = hlnkB36; hlnk1[37] = hlnkB37; hlnk1[38] = hlnkB38; hlnk1[39] = hlnkB39; hlnk1[40] = hlnkB40;
                    hlnk1[41] = hlnkB41; hlnk1[42] = hlnkB42; hlnk1[43] = hlnkB43; hlnk1[44] = hlnkB44; hlnk1[45] = hlnkB45;
                    hlnk1[46] = hlnkB46; hlnk1[47] = hlnkB47; hlnk1[48] = hlnkB48; hlnk1[49] = hlnkB49; hlnk1[50] = hlnkB50;
                    hlnk1[51] = hlnkB51; hlnk1[52] = hlnkB52; hlnk1[53] = hlnkB53; hlnk1[54] = hlnkB54; hlnk1[55] = hlnkB55;
                    hlnk1[56] = hlnkB56; hlnk1[57] = hlnkB57; hlnk1[58] = hlnkB58; hlnk1[59] = hlnkB59; hlnk1[60] = hlnkB60;



                    lbli[1] = hlnkB01i; lbli[2] = hlnkB02i; lbli[3] = hlnkB03i; lbli[4] = hlnkB04i; lbli[5] = hlnkB05i;
                    lbli[6] = hlnkB06i; lbli[7] = hlnkB07i; lbli[8] = hlnkB08i; lbli[9] = hlnkB09i; lbli[10] = hlnkB10i;
                    lbli[11] = hlnkB11i; lbli[12] = hlnkB12i; lbli[13] = hlnkB13i; lbli[14] = hlnkB14i; lbli[15] = hlnkB15i;
                    lbli[16] = hlnkB16i; lbli[17] = hlnkB17i; lbli[18] = hlnkB18i; lbli[19] = hlnkB19i; lbli[20] = hlnkB20i;
                    lbli[21] = hlnkB21i; lbli[22] = hlnkB22i; lbli[23] = hlnkB23i; lbli[24] = hlnkB24i; lbli[25] = hlnkB25i;
                    lbli[26] = hlnkB26i; lbli[27] = hlnkB27i; lbli[28] = hlnkB28i; lbli[29] = hlnkB29i; lbli[30] = hlnkB30i;
                    lbli[31] = hlnkB31i; lbli[32] = hlnkB32i; lbli[33] = hlnkB33i; lbli[34] = hlnkB34i; lbli[35] = hlnkB35i;
                    lbli[36] = hlnkB36i; lbli[37] = hlnkB37i; lbli[38] = hlnkB38i; lbli[39] = hlnkB39i; lbli[40] = hlnkB40i;
                    lbli[41] = hlnkB41i; lbli[42] = hlnkB42i; lbli[43] = hlnkB43i; lbli[44] = hlnkB44i; lbli[45] = hlnkB45i;
                    lbli[46] = hlnkB46i; lbli[47] = hlnkB47i; lbli[48] = hlnkB48i; lbli[49] = hlnkB49i; lbli[50] = hlnkB50i;
                    lbli[51] = hlnkB51i; lbli[52] = hlnkB52i; lbli[53] = hlnkB53i; lbli[54] = hlnkB54i; lbli[55] = hlnkB55i;
                    lbli[56] = hlnkB56i; lbli[57] = hlnkB57i; lbli[58] = hlnkB58i; lbli[59] = hlnkB59i; lbli[60] = hlnkB60i;

                }
                if (i == 4)
                {
                    hlnk1[0] = hlnkC00; hlnk1[1] = hlnkC01; hlnk1[2] = hlnkC02; hlnk1[3] = hlnkC03; hlnk1[4] = hlnkC04;
                    hlnk1[5] = hlnkC05; hlnk1[6] = hlnkC06; hlnk1[7] = hlnkC07; hlnk1[8] = hlnkC08; hlnk1[9] = hlnkC09;
                    hlnk1[10] = hlnkC10; hlnk1[11] = hlnkC11; hlnk1[12] = hlnkC12; hlnk1[13] = hlnkC13; hlnk1[14] = hlnkC14;
                    hlnk1[15] = hlnkC15; hlnk1[16] = hlnkC16; hlnk1[17] = hlnkC17; hlnk1[18] = hlnkC18; hlnk1[19] = hlnkC19;
                    hlnk1[20] = hlnkC20; hlnk1[21] = hlnkC21; hlnk1[22] = hlnkC22; hlnk1[23] = hlnkC23; hlnk1[24] = hlnkC24; hlnk1[25] = hlnkC25;
                    hlnk1[26] = hlnkC26; hlnk1[27] = hlnkC27; hlnk1[28] = hlnkC28; hlnk1[29] = hlnkC29; hlnk1[30] = hlnkC30;
                    hlnk1[31] = hlnkC31; hlnk1[32] = hlnkC32; hlnk1[33] = hlnkC33; hlnk1[34] = hlnkC34; hlnk1[35] = hlnkC35;
                    hlnk1[36] = hlnkC36; hlnk1[37] = hlnkC37; hlnk1[38] = hlnkC38; hlnk1[39] = hlnkC39; hlnk1[40] = hlnkC40;
                    hlnk1[41] = hlnkC41; hlnk1[42] = hlnkC42; hlnk1[43] = hlnkC43; hlnk1[44] = hlnkC44; hlnk1[45] = hlnkC45;
                    hlnk1[46] = hlnkC46; hlnk1[47] = hlnkC47; hlnk1[48] = hlnkC48; hlnk1[49] = hlnkC49; hlnk1[50] = hlnkC50;
                    hlnk1[51] = hlnkC51; hlnk1[52] = hlnkC52; hlnk1[53] = hlnkC53; hlnk1[54] = hlnkC54; hlnk1[55] = hlnkC55;
                    hlnk1[56] = hlnkC56; hlnk1[57] = hlnkC57; hlnk1[58] = hlnkC58; hlnk1[59] = hlnkC59; hlnk1[60] = hlnkC60;



                    lbli[1] = hlnkC01i; lbli[2] = hlnkC02i; lbli[3] = hlnkC03i; lbli[4] = hlnkC04i; lbli[5] = hlnkC05i;
                    lbli[6] = hlnkC06i; lbli[7] = hlnkC07i; lbli[8] = hlnkC08i; lbli[9] = hlnkC09i; lbli[10] = hlnkC10i;
                    lbli[11] = hlnkC11i; lbli[12] = hlnkC12i; lbli[13] = hlnkC13i; lbli[14] = hlnkC14i; lbli[15] = hlnkC15i;
                    lbli[16] = hlnkC16i; lbli[17] = hlnkC17i; lbli[18] = hlnkC18i; lbli[19] = hlnkC19i; lbli[20] = hlnkC20i;
                    lbli[21] = hlnkC21i; lbli[22] = hlnkC22i; lbli[23] = hlnkC23i; lbli[24] = hlnkC24i; lbli[25] = hlnkC25i;
                    lbli[26] = hlnkC26i; lbli[27] = hlnkC27i; lbli[28] = hlnkC28i; lbli[29] = hlnkC29i; lbli[30] = hlnkC30i;
                    lbli[31] = hlnkC31i; lbli[32] = hlnkC32i; lbli[33] = hlnkC33i; lbli[34] = hlnkC34i; lbli[35] = hlnkC35i;
                    lbli[36] = hlnkC36i; lbli[37] = hlnkC37i; lbli[38] = hlnkC38i; lbli[39] = hlnkC39i; lbli[40] = hlnkC40i;
                    lbli[41] = hlnkC41i; lbli[42] = hlnkC42i; lbli[43] = hlnkC43i; lbli[44] = hlnkC44i; lbli[45] = hlnkC45i;
                    lbli[46] = hlnkC46i; lbli[47] = hlnkC47i; lbli[48] = hlnkC48i; lbli[49] = hlnkC49i; lbli[50] = hlnkC50i;
                    lbli[51] = hlnkC51i; lbli[52] = hlnkC52i; lbli[53] = hlnkC53i; lbli[54] = hlnkC54i; lbli[55] = hlnkC55i;
                    lbli[56] = hlnkC56i; lbli[57] = hlnkC57i; lbli[58] = hlnkC58i; lbli[59] = hlnkC59i; lbli[60] = hlnkC60i;
                }

                for (int j = 0; j < tbl2.Rows.Count; j++)
                {
                    hlnk1[j].Text = tbl2.Rows[j]["itemdesc"].ToString();
                    hlnk1[j].NavigateUrl = tbl2.Rows[j]["itemurl"].ToString();


                    if (tbl2.Rows[j]["fbold"].ToString().Trim() == "mb")
                    {
                        hlnk1[j].Style.Add("font-weight", "bold");
                        hlnk1[j].Style.Add("font-size", "16px");

                    }
                    else if (tbl2.Rows[j]["fbold"].ToString().Trim() == "b")
                    {
                        hlnk1[j].Style.Add("font-weight", "bold");
                        hlnk1[j].Style.Add("font-size", "14px");
                        hlnk1[j].Style.Add("color", "yellow");

                    }
                    else
                    {
                        hlnk1[j].Style.Add("font-weight", "regular");
                        hlnk1[j].Style.Add("font-size", "12px");
                        hlnk1[j].Style.Add("color", "black");
                    }

                    hlnk1[j].Visible = true;





                    if (j != 0)
                    {
                        lbli[j].Visible = (hlnk1[j].Text.Length > 0 && Convert.ToBoolean(tbl2.Rows[j]["itemslct"]).ToString() == "True") ? true : false;

                    }





                }
                lbl1[i - 2].Visible = hlnk1[0].Visible;
            }
            this.GenerateMenuItems1(tbl1);
        }

        protected void GenerateMenuItems1(DataTable tbl1)
        {
            //DataTable tbl1 = ConstantInfo.MenuTable("10ACC");


            MenuItem FirstParentItem = null;
            //MenuItem SecondParentItem = null;
            //MenuItem ThirdParentItem = null;
            //MenuItem FourthParentItem = null;
            //MenuItem FifthParentItem = null;
            Menu Menu1 = ((Menu)this.Master.FindControl("Menu1"));
            Menu1.Items.Clear();

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string id = tbl1.Rows[i]["itemcod"].ToString();
                string desc = tbl1.Rows[i]["itemdesc"].ToString().Trim();
                string url = tbl1.Rows[i]["itemurl"].ToString();
                bool slct = Convert.ToBoolean(tbl1.Rows[i]["itemslct"]);
                if (desc.Trim().Length == 0)
                    continue;

                if (id.Substring(2, 8) == "00000000")
                {
                    FirstParentItem = new MenuItem(desc);
                    FirstParentItem.Value = id;
                    FirstParentItem.Selectable = slct;
                    FirstParentItem.NavigateUrl = url;
                    Menu1.Items.Add(FirstParentItem);
                }
                //else if (id.Substring(4, 6) == "000000")
                //{
                //    SecondParentItem = new MenuItem(desc);
                //    SecondParentItem.Value = id;
                //    SecondParentItem.Selectable = slct;
                //    SecondParentItem.NavigateUrl = url;
                //    FirstParentItem.ChildItems.Add(SecondParentItem);
                //}
                //else if (id.Substring(6, 4) == "0000")
                //{
                //    ThirdParentItem = new MenuItem(desc);
                //    ThirdParentItem.Value = id;
                //    ThirdParentItem.Selectable = slct;
                //    ThirdParentItem.NavigateUrl = url;
                //    SecondParentItem.ChildItems.Add(ThirdParentItem);
                //}
                //else if (id.Substring(8, 2) == "00")
                //{
                //    FourthParentItem = new MenuItem(desc);
                //    FourthParentItem.Value = id;
                //    FourthParentItem.Selectable = slct;
                //    FourthParentItem.NavigateUrl = url;
                //    ThirdParentItem.ChildItems.Add(FourthParentItem);
                //}
                //else
                //{
                //    FifthParentItem = new MenuItem(desc);
                //    FifthParentItem.Value = id;
                //    FifthParentItem.Selectable = slct;
                //    FifthParentItem.NavigateUrl = url;
                //    FourthParentItem.ChildItems.Add(FifthParentItem);
                //}
            }
        }



    }
}