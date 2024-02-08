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


namespace SPEWEB.F_31_Mis
{
    public partial class LinkExportDocs : System.Web.UI.Page
    {
        ProcessAccess Shipment = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.txtEXPORTDT.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                this.txtBLAWBDT.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                this.txtExfacdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                this.ShowShipline();
                this.LoadDataLC_Shipment();
                this.ShowShiplineType();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export With Documentation Information Entry";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintReport_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void ShowShipline()
        {
            string comcod = GetComCode();
            DataSet ds6 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETSHIPLINE", "", "", "", "", "", "", "", "", "");

            if (ds6 == null)
                return;
            this.ddlShipLine.DataTextField = "sirdesc";
            this.ddlShipLine.DataValueField = "sircode";
            this.ddlShipLine.DataSource = ds6.Tables[0];
            this.ddlShipLine.DataBind();

        }

        private void ShowShiplineType()
        {
            string comcod = GetComCode();
            DataSet ds6 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETSHIPLINETYPE", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.DDLShipmentType.DataTextField = "sirdesc";
            this.DDLShipmentType.DataValueField = "sircode";
            this.DDLShipmentType.DataSource = ds6.Tables[0];
            this.DDLShipmentType.DataBind();

        }

        private string GetComCode()
        {


            return (this.Request.QueryString["comcod"].ToString());

        }

        protected void lbtnPrevList_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            string date1 = this.txtDate.Text.Substring(0, 11);
            DataSet ds1 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "ExpInvIdList", date1, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.DDLPrevIDList.DataTextField = "IdDatNo";
            this.DDLPrevIDList.DataValueField = "invno";
            this.DDLPrevIDList.DataSource = ds1.Tables[0];
            this.DDLPrevIDList.DataBind();
            if (this.DDLMasterLC.Items.Count == 0)
                this.LoadDataLC_Shipment();

        }

        private void LoadDataLC_Shipment()
        {
            string comcod = GetComCode();
            DataSet ds1 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "ExisLCOrderShip", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.DDLMasterLC.DataTextField = "actdesc1";
            this.DDLMasterLC.DataValueField = "actcode";
            this.DDLMasterLC.DataSource = ds1.Tables[0];
            this.DDLMasterLC.DataBind();

            Session["TblOrder"] = ds1.Tables[1];
            this.DDLShipment.DataTextField = "ShipDesc";
            this.DDLShipment.DataValueField = "shipmid";
            this.DDLShipment.DataSource = ds1.Tables[2];
            this.DDLShipment.DataBind();

            Session["TblShipType"] = ds1.Tables[3];

            DDLMasterLC_SelectedIndexChanged(null, null);
        }

        protected void lbtbExport_Click(object sender, EventArgs e)
        {
            if (this.lbtbExport.Text == "Ok")
            {
                this.lbtbExport.Text = "New";

                if (this.DDLPrevIDList.Items.Count > 0)
                {
                    this.lblInvID.Text = this.DDLPrevIDList.SelectedValue.ToString().Trim();

                    this.show_ShipmentInformation(true);
                    this.DDLMasterLC.Visible = false;
                    this.DDLOrder.Visible = false;
                    this.DDLShipment.Visible = false;
                    this.DDLShipmentType.Visible = false;
                    this.lblMLC.Visible = true;
                    this.lblOrderNo.Visible = true;
                    this.lblShipmentName.Visible = true;
                    this.lblShipmentType.Text = this.DDLShipmentType.SelectedItem.ToString();
                    this.lblShipmentType.Visible = true;
                    return;
                }
                string comcod = GetComCode();
                DataSet ds1 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "LastInvNo", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblInvID.Text = ds1.Tables[0].Rows[0]["NewInvNo"].ToString();

                this.show_ShipmentInformation(true);
                this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString().Substring(14);
                this.lblOrderNo.Text = this.DDLOrder.SelectedItem.ToString();
                this.lblShipmentName.Text = this.DDLShipment.SelectedItem.ToString();
                this.lblShipmentType.Text = this.DDLShipmentType.SelectedItem.ToString();
                this.lblPrelist.Visible = false;
                this.txtPrevSearch.Visible = false;
                this.lbtnPrevList.Visible = false;
                this.DDLPrevIDList.Visible = false;

                this.DDLMasterLC.Visible = false;
                this.DDLOrder.Visible = false;
                this.DDLShipment.Visible = false;
                this.DDLShipmentType.Visible = false;
                this.lblMLC.Visible = true;
                this.lblOrderNo.Visible = true;
                this.lblShipmentName.Visible = true;
                this.lblShipmentType.Visible = true;
            }
            else
            {
                this.lbtbExport.Text = "Ok";
                this.lblMessage.Text = "";

                this.DDLPrevIDList.Items.Clear();
                this.PanelLC2.Visible = false;
                this.lblInvID.Text = "";
                this.txtRefNo.Text = "";
                gv1.DataSource = null;
                gv1.DataBind();
                this.lblPrelist.Visible = true;
                this.txtPrevSearch.Visible = true;
                this.lbtnPrevList.Visible = true;
                this.DDLPrevIDList.Visible = true;
                this.DDLMasterLC.Visible = true;
                this.DDLOrder.Visible = true;
                this.lblMLC.Visible = false;
                this.lblOrderNo.Visible = false;
                this.DDLShipment.Visible = true;
                this.lblShipmentName.Visible = false;
                this.DDLShipmentType.Visible = true;
                this.lblShipmentType.Visible = false;
                this.PanelLC2.Visible = false;
                this.rblFwl01.SelectedIndex = 0;
                this.rblFwl02.SelectedIndex = 0;
                this.rblFwl03.SelectedIndex = 0;
                this.rblFwl04.SelectedIndex = 0;
                this.rblFwl05.SelectedIndex = 0;
                this.rblFwl06.SelectedIndex = 0;
                this.rblFwl07.SelectedIndex = 0;
                this.rblFwl08.SelectedIndex = 0;
                this.rblFwl09.SelectedIndex = 0;
                this.rblFwl10.SelectedIndex = 0;
                Session.Remove("tblInvPrn01");
                Session.Remove("tblInvPrn02");
                Session.Remove("tblInvPrn03");
                this.ClearScreen();
                this.ShowShiplineType();

            }

        }

        private void ClearScreen()
        {
            this.txtEXPORTNO.Text = "";
            this.txtEXPORTDT.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtBLAWBNO.Text = "";
            this.txtBLAWBDT.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtCNTNRNO.Text = "";
            this.txtINVRMRKS.Text = "";
            this.txtSHIPMARK.Text = "";
            this.txtTQTYDES.Text = "";
            this.txtTNWTDES.Text = "";
            this.txtTGWTDES.Text = "";
            this.txtTCBMDES.Text = "";
            this.txtMSURMNT.Text = "";



        }
        protected void DDLMasterLC_SelectedIndexChanged(object sender, EventArgs e)
        {

            string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
            DataView dv1 = ((DataTable)Session["TblOrder"]).DefaultView;
            dv1.RowFilter = "mlccod = '" + mlccod + "'";
            this.DDLOrder.DataTextField = "GenData";
            this.DDLOrder.DataValueField = "gencod1";
            this.DDLOrder.DataSource = dv1;
            this.DDLOrder.DataBind();

            DataView dv3 = ((DataTable)Session["TblShipType"]).DefaultView;
            dv1.RowFilter = "mlccod = '" + mlccod + "'";
            this.DDLShipmentType.DataTextField = "shiptype";
            this.DDLShipmentType.DataValueField = "gencod1";
            this.DDLShipmentType.DataSource = dv3;
            this.DDLShipmentType.DataBind();

            this.shipmentType();


        }
        protected void shipmentType()
        {
            string comcod = GetComCode();
            string mlcno = this.DDLOrder.SelectedValue.ToString();
            DataSet ds2 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "SHIPMENT", mlcno, "", "", "", "", "", "", "", "");
            this.DDLShipment.DataTextField = "ShipDesc";
            this.DDLShipment.DataValueField = "shipmid";
            this.DDLShipment.DataSource = ds2.Tables[0];
            this.DDLShipment.DataBind();

        }
        protected void shipmentType01(string invo)
        {
            string comcod = GetComCode();
            string mlcno = this.DDLOrder.SelectedValue.ToString();
            DataSet ds2 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "SHIPMENT01", mlcno, invo, "", "", "", "", "", "", "");
            this.DDLShipment.DataTextField = "ShipDesc";
            this.DDLShipment.DataValueField = "shipmid";
            this.DDLShipment.DataSource = ds2.Tables[0];
            this.DDLShipment.DataBind();

        }
        private void show_ShipmentInformation(bool mEntryMode)
        {
            if (this.DDLMasterLC.SelectedValue == null || this.DDLOrder.SelectedValue == null ||
                this.DDLShipment.SelectedValue == null)
            {
                return;
            }
            if (this.DDLMasterLC.SelectedValue.ToString().Trim().Length == 0 || this.DDLOrder.SelectedValue.ToString().Trim().Length == 0 ||
                 this.DDLShipment.SelectedValue.ToString().Trim().Length == 0)
            {
                return;
            }
            //string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
            string mlccod = this.DDLOrder.SelectedValue.ToString();
            string shipmid = this.DDLShipment.SelectedValue.ToString().Trim();
            string invid = this.lblInvID.Text.Trim();
            string comcod = GetComCode();
            DataSet ds1 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GetActualShipmentDetails", mlccod, "", shipmid, invid, "", "", "", "", "");

            if (ds1 == null)
                return;

            if (ds1.Tables[2].Rows.Count > 0)
            {
                this.DDLMasterLC.SelectedValue = ds1.Tables[2].Rows[0]["mlccod"].ToString();
                DDLMasterLC_SelectedIndexChanged(null, null);
                this.DDLOrder.SelectedValue = ds1.Tables[2].Rows[0]["ordrid"].ToString();
                this.shipmentType01(invid);
                //this.DDLShipment.SelectedValue = ds1.Tables[2].Rows[0]["shipmid"].ToString();
                this.DDLShipment.Items.Clear();

                this.DDLShipment.DataTextField = "shipdesc";
                this.DDLShipment.DataValueField = "shipmid";
                this.DDLShipment.DataSource = ds1.Tables[2];
                this.DDLShipment.DataBind();

                this.txtDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy");
                this.txtRefNo.Text = ds1.Tables[2].Rows[0]["invno2"].ToString();
                this.txtEXPORTNO.Text = ds1.Tables[2].Rows[0]["exportno"].ToString();
                this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString().Substring(13);
                this.lblOrderNo.Text = this.DDLOrder.SelectedItem.ToString();
                this.lblShipmentName.Text = this.DDLShipment.SelectedItem.ToString();
                DateTime dt1 = Convert.ToDateTime(ds1.Tables[2].Rows[0]["exportdt"]);


                this.txtEXPORTDT.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["exportdt"]).ToString("dd-MMM-yyyy");
                this.txtBLAWBNO.Text = ds1.Tables[2].Rows[0]["blawbno"].ToString();
                this.txtBLAWBDT.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["blawbdt"]).ToString("dd-MMM-yyyy");
                this.txtCNTNRNO.Text = ds1.Tables[2].Rows[0]["cntnrno"].ToString();
                this.txtINVRMRKS.Text = ds1.Tables[2].Rows[0]["invrmrks"].ToString();
                this.txtSHIPMARK.Text = ds1.Tables[2].Rows[0]["shipmark"].ToString();
                this.txtTQTYDES.Text = ds1.Tables[2].Rows[0]["tqtydes"].ToString();
                this.txtTNWTDES.Text = ds1.Tables[2].Rows[0]["tnwtdes"].ToString();
                this.txtTGWTDES.Text = ds1.Tables[2].Rows[0]["tgwtdes"].ToString();
                this.txtTCBMDES.Text = ds1.Tables[2].Rows[0]["tcbmdes"].ToString();
                this.txtMSURMNT.Text = ds1.Tables[2].Rows[0]["msurmnt"].ToString();
                this.txtExfacdate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["exfacdt"]).ToString("dd-MMM-yyyy");
                this.ddlShipLine.SelectedValue = ds1.Tables[2].Rows[0]["shipline"].ToString();
                this.PanelLC2.Visible = true;
            }
            for (int i = 0; i < 15; i++)
            {
                this.gv1.Columns[i + 6].HeaderText = "";
                this.gv1.Columns[i + 6].Visible = false;
            }

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {
                string ColHeadDesc = ds1.Tables[1].Rows[i]["GenData"].ToString();
                this.gv1.Columns[i + 6].HeaderText = ColHeadDesc;
                this.gv1.Columns[i + 6].Visible = true;
            }



            //string mStyleID2 = "xxxxxxx";
            //string mColorID2 = "xxxxxxx";
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID2)
            //        ds1.Tables[0].Rows[i]["StyleDes"] = " >> ";
            //    if (ds1.Tables[0].Rows[i]["styleid"].ToString() +
            //        ds1.Tables[0].Rows[i]["colorid"].ToString() == mStyleID2 + mColorID2)
            //        ds1.Tables[0].Rows[i]["Desc1"] = " >";

            //    mStyleID2 = ds1.Tables[0].Rows[i]["styleid"].ToString();
            //    mColorID2 = ds1.Tables[0].Rows[i]["colorid"].ToString();
            //}



            //DataTable tbl01 = ds1.Tables[0];
            //DataTable tbl02 = ds1.Tables[0].Copy();
            //for (int j = 0; j < tbl02.Rows.Count; j++)
            //{
            //    string mStyleID = tbl02.Rows[j]["styleid"].ToString().Trim();
            //    string mColorID = tbl02.Rows[j]["colorid"].ToString().Trim();
            //    string mUnit1 = tbl02.Rows[j]["Unit1"].ToString().Trim();
            //    string mCntrGrp = tbl02.Rows[j]["crtngrp"].ToString().Trim();
            //    string mNewRowSl = Convert.ToInt32(Convert.ToInt32(mCntrGrp) + 1).ToString("00");
            //    if (Convert.ToDouble(tbl02.Rows[j]["Total1"]) > 0 && mEntryMode)
            //    {
            //        bool mRowFound = false;
            //        for (int i = 0; i < tbl01.Rows.Count; i++)
            //        {
            //            string mStyleID1 = tbl01.Rows[i]["styleid"].ToString().Trim();
            //            string mColorID1 = tbl01.Rows[i]["colorid"].ToString().Trim();
            //            string mCntrGrp1 = tbl01.Rows[i]["crtngrp"].ToString().Trim();
            //            if (mStyleID + mColorID + mNewRowSl == mStyleID1 + mColorID1 + mCntrGrp1)
            //            {
            //                mRowFound = true;
            //                break;
            //            }
            //        }
            //        if (!mRowFound)
            //        {
            //            DataRow dr1 = tbl01.NewRow();
            //            dr1["styleid"] = mStyleID;
            //            dr1["colorid"] = mColorID;
            //            dr1["StyleDes"] = ">>";
            //            dr1["Desc1"] = ">";
            //            dr1["Unit1"] = mUnit1;
            //            dr1["crtngrp"] = mNewRowSl;
            //            dr1["F7201001"] = 0; dr1["F7201002"] = 0; dr1["F7201003"] = 0; dr1["F7201004"] = 0;
            //            dr1["F7201005"] = 0; dr1["F7201006"] = 0; dr1["F7201007"] = 0; dr1["F7201008"] = 0;
            //            dr1["F7201009"] = 0; dr1["F7201010"] = 0; dr1["F7201011"] = 0; dr1["F7201012"] = 0;
            //            dr1["F7201013"] = 0; dr1["F7201014"] = 0; dr1["F7201015"] = 0; dr1["Total1"] = 0;
            //            dr1["crtnqty"] = 0;
            //            dr1["crtnslno"] = "";
            //            tbl01.Rows.Add(dr1);
            //        }
            //    }
            //}
            ////////////////////////
            //DataView ddv1 = tbl01.DefaultView;
            //ddv1.Sort = "styleid, colorid, crtngrp";
            //Session["tblShip"] = ddv1.ToTable();
            Session["tblShip"] = this.HiddenSameData(ds1.Tables[0]);
            this.gv1_CalculateFootertotal(gv1, ((DataTable)Session["tblShip"]));
            this.gv1.DataSource = ((DataTable)Session["tblShip"]);
            this.gv1.DataBind();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string styleid = dt1.Rows[0]["styleid"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["styleid"].ToString() == styleid)
                {
                    styleid = dt1.Rows[j]["styleid"].ToString();
                    dt1.Rows[j]["StyleDes"] = "";
                    dt1.Rows[j]["Unit1"] = "";
                }

                else
                    styleid = dt1.Rows[j]["styleid"].ToString();
            }
            return dt1;

        }


        protected void lbtnFUpdate_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mLccod = this.DDLOrder.SelectedValue.ToString().Trim();
            string mShipmid = this.DDLShipment.SelectedValue.ToString().Trim();
            string mInvid = this.lblInvID.Text.Trim();
            string mInvid2 = this.txtRefNo.Text.Trim();
            string mShipmdat = this.txtDate.Text.Trim().Substring(0, 11);
            string mExportno = this.txtEXPORTNO.Text.Trim();
            string mExportdt = (this.txtEXPORTDT.Text.Trim().Length == 0 ? "01-JAN-1900" : this.txtEXPORTDT.Text.Trim().Substring(0, 11));
            string mBlawbno = this.txtBLAWBNO.Text.Trim();
            string mBlawbdt = (this.txtBLAWBDT.Text.Trim().Length == 0 ? "01-JAN-1900" : this.txtBLAWBDT.Text.Trim().Substring(0, 11));
            string mCntnrno = this.txtCNTNRNO.Text.Trim();
            string mInvrmrks = this.txtINVRMRKS.Text.Trim();
            string mShipmark = this.txtSHIPMARK.Text.Trim();
            string mTqtydes = this.txtTQTYDES.Text.Trim();
            string mTnwtdes = this.txtTNWTDES.Text.Trim();
            string mTgwtdes = this.txtTGWTDES.Text.Trim();
            string mTcbmdes = this.txtTCBMDES.Text.Trim();
            string mMsurmnt = this.txtMSURMNT.Text.Trim();
            string exfacdate = this.txtExfacdate.Text.Trim().Substring(0, 11);
            string shipline = this.ddlShipLine.SelectedValue.ToString();
            string shippingType = this.DDLShipmentType.SelectedValue.ToString();

            string comcod = GetComCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string entrydat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = Shipment.UpdateTransInfo1(comcod, "SP_ENTRY_EXPORT_DOCS", "SaveActualShipmentInfo", mLccod, mInvid, mInvid2, "",
                            mShipmid, mShipmdat, mExportno, mExportdt, mBlawbno, mBlawbdt, mCntnrno, mInvrmrks, mShipmark,
                            mTqtydes, mTnwtdes, mTgwtdes, mTcbmdes, mMsurmnt, exfacdate, shipline, userid, Terminal, Sessionid, entrydat, shippingType, "");
            //bool result = _newLC.UpdateShipmentInfo(ConstantInfo.ComCode, "SaveActualShipmentInfo", mLccod,
            //    mInvid, mInvid2, mOrderid, mShipmid, mShipmdat, mExportno, mExportdt, mBlawbno, mBlawbdt,
            //    mCntnrno, mInvrmrks, mShipmark, mTqtydes, mTnwtdes, mTgwtdes, mTcbmdes, mMsurmnt, "", "", "", "", "", "");
            if (!result)
            {
                this.lblMessage.Text = Shipment.ErrorObject["Msg"].ToString();
                return;
            }

            DataTable tbl01 = ((DataTable)Session["tblShip"]);
            for (int i = 0; i < tbl01.Rows.Count; i++)
            {
                string mStyleID = tbl01.Rows[i]["styleid"].ToString();
                string mColorID = tbl01.Rows[i]["colorid"].ToString();
                string mCrtngrp = tbl01.Rows[i]["crtngrp"].ToString();
                string mF7201001 = tbl01.Rows[i]["F7201001"].ToString();
                string mF7201002 = tbl01.Rows[i]["F7201002"].ToString();
                string mF7201003 = tbl01.Rows[i]["F7201003"].ToString();
                string mF7201004 = tbl01.Rows[i]["F7201004"].ToString();
                string mF7201005 = tbl01.Rows[i]["F7201005"].ToString();
                string mF7201006 = tbl01.Rows[i]["F7201006"].ToString();
                string mF7201007 = tbl01.Rows[i]["F7201007"].ToString();
                string mF7201008 = tbl01.Rows[i]["F7201008"].ToString();
                string mF7201009 = tbl01.Rows[i]["F7201009"].ToString();
                string mF7201010 = tbl01.Rows[i]["F7201010"].ToString();
                string mF7201011 = tbl01.Rows[i]["F7201011"].ToString();
                string mF7201012 = tbl01.Rows[i]["F7201012"].ToString();
                string mF7201013 = tbl01.Rows[i]["F7201013"].ToString();
                string mF7201014 = tbl01.Rows[i]["F7201014"].ToString();
                string mF7201015 = tbl01.Rows[i]["F7201015"].ToString();
                string mCrtnqty = tbl01.Rows[i]["crtnqty"].ToString();
                string mCrtnslno = tbl01.Rows[i]["crtnslno"].ToString();

                result = Shipment.UpdateTransInfo1(comcod, "SP_ENTRY_EXPORT_DOCS", "SaveActualShipmentDetails",
                         mLccod, mInvid, mStyleID, mColorID, mF7201001, mF7201002, mF7201003, mF7201004,
                         mF7201005, mF7201006, mF7201007, mF7201008, mF7201009, mF7201010, mF7201011,
                         mF7201012, mF7201013, mF7201014, mF7201015, mCrtnqty, mCrtnslno, mShipmdat,
                         mCrtngrp, shippingType, "", "");
                if (!result)
                {
                    this.lblMessage.Text = Shipment.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.lblMessage.Text = "Updated Successfully";
        }
        protected void gv1_CalculateFootertotal(GridView gv1a, DataTable tbl01a)
        {
            gv1a.Columns[6].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201001)", "")) ?
                0 : tbl01a.Compute("sum(F7201001)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[7].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201002)", "")) ?
                0 : tbl01a.Compute("sum(F7201002)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[8].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201003)", "")) ?
                0 : tbl01a.Compute("sum(F7201003)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[9].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201004)", "")) ?
                0 : tbl01a.Compute("sum(F7201004)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[10].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201005)", "")) ?
                0 : tbl01a.Compute("sum(F7201005)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[11].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201006)", "")) ?
                0 : tbl01a.Compute("sum(F7201006)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[12].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201007)", "")) ?
                0 : tbl01a.Compute("sum(F7201007)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[13].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201008)", "")) ?
                0 : tbl01a.Compute("sum(F7201008)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[14].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201009)", "")) ?
                0 : tbl01a.Compute("sum(F7201009)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[15].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201010)", "")) ?
                0 : tbl01a.Compute("sum(F7201010)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[16].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201011)", "")) ?
                0 : tbl01a.Compute("sum(F7201011)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[17].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201012)", "")) ?
                0 : tbl01a.Compute("sum(F7201012)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[18].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201013)", "")) ?
                0 : tbl01a.Compute("sum(F7201013)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[19].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201014)", "")) ?
                0 : tbl01a.Compute("sum(F7201014)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[20].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201015)", "")) ?
                0 : tbl01a.Compute("sum(F7201015)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[21].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(Total1)", "")) ?
                0 : tbl01a.Compute("sum(Total1)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[22].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(crtnqty)", "")) ?
                0 : tbl01a.Compute("sum(crtnqty)", ""))).ToString("#,##0;(#,##0); ");

            //this.gv1.Columns[5].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01.Compute("sum(F7201001)", "")) ?
            //            0.00 : tbl01.Compute("sum(F7201001)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }



        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {

            if (Session["tblInvPrn01"] == null)
            {
                string mlccod = this.DDLOrder.SelectedValue.ToString().Trim();
                string invid = this.lblInvID.Text.Trim();
                if (invid.Length == 0)
                    return;
                string comcod = GetComCode();
                DataSet ds2 = Shipment.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "ExpDocPrint", mlccod, invid, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                Session["tblInvPrn01"] = ds2.Tables[0];
                Session["tblInvPrn02"] = ds2.Tables[1];
                Session["tblInvPrn03"] = ds2.Tables[2];
            }

            //ReportDocument mRpt1 = new ReportDocument();
            ////switch (this.DDLDocName.SelectedIndex)
            ////{
            ////    case 0:
            ////        mRpt1 = CommercialInvoice();
            ////        new rptForwLetter();
            ////        break;
            ////    case 1:
            ////        mRpt1 = ForwardingLetter(); // new rptComInvoice();
            ////        break;
            ////    case 2:
            ////        mRpt1 = ComPackingList(); // new rptComPackList();
            ////        break;
            ////    case 3:
            ////        mRpt1 = BillofExchange();// new prtBillofExchange();
            ////        break;
            ////}
            //Session["Report1"] = mRpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        //private ReportDocument ComPackingList()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument mRpt2 = new RMGiRPT.R_03_CostABgd.rptComPackList();

        //    DataTable mTbl01 = ((DataTable)Session["tblInvPrn01"]);
        //    DataTable mTbl02 = ((DataTable)Session["tblInvPrn02"]);
        //    DataTable mTbl03 = ((DataTable)Session["tblInvPrn03"]);
        //    mRpt2.SetDataSource(mTbl02);
        //    TextObject txtf7201001 = mRpt2.ReportDefinition.ReportObjects["txtf7201001"] as TextObject;
        //    txtf7201001.Text = this.gv1.Columns[6].HeaderText.ToString();

        //    TextObject txtf7201002 = mRpt2.ReportDefinition.ReportObjects["txtf7201002"] as TextObject;
        //    txtf7201002.Text = this.gv1.Columns[7].HeaderText.ToString();

        //    TextObject txtf7201003 = mRpt2.ReportDefinition.ReportObjects["txtf7201003"] as TextObject;
        //    txtf7201003.Text = this.gv1.Columns[8].HeaderText.ToString();

        //    TextObject txtf7201004 = mRpt2.ReportDefinition.ReportObjects["txtf7201004"] as TextObject;
        //    txtf7201004.Text = this.gv1.Columns[9].HeaderText.ToString();

        //    TextObject txtf7201005 = mRpt2.ReportDefinition.ReportObjects["txtf7201005"] as TextObject;
        //    txtf7201005.Text = this.gv1.Columns[10].HeaderText.ToString();

        //    TextObject txtf7201006 = mRpt2.ReportDefinition.ReportObjects["txtf7201006"] as TextObject;
        //    txtf7201006.Text = this.gv1.Columns[11].HeaderText.ToString();
        //    //txtf7201001
        //    TextObject txtINVNO2 = mRpt2.ReportDefinition.ReportObjects["txtINVNO2"] as TextObject;
        //    txtINVNO2.Text = mTbl03.Rows[0]["invno2"].ToString();

        //    TextObject txtSHIPMDAT = mRpt2.ReportDefinition.ReportObjects["txtSHIPMDAT"] as TextObject;
        //    txtSHIPMDAT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy");

        //    TextObject txtEXPORTNO = mRpt2.ReportDefinition.ReportObjects["txtEXPORTNO"] as TextObject;
        //    txtEXPORTNO.Text = mTbl03.Rows[0]["exportno"].ToString();

        //    TextObject txtEXPORTDT = mRpt2.ReportDefinition.ReportObjects["txtEXPORTDT"] as TextObject;
        //    txtEXPORTDT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["exportdt"]).ToString("dd-MMM-yyyy");

        //    TextObject txtMLCNo = mRpt2.ReportDefinition.ReportObjects["txtMLCNo"] as TextObject;
        //    txtMLCNo.Text = mTbl01.Rows[0]["mlcdes"].ToString().Trim();

        //    DataRow[] DrLCDate = mTbl01.Select("gencod='010100101001'");
        //    string mLCDate = Convert.ToDateTime(DrLCDate[0]["dateval"]).ToString("dd-MMM-yyyy");

        //    TextObject txtMLCDate = mRpt2.ReportDefinition.ReportObjects["txtMLCDate"] as TextObject;
        //    txtMLCDate.Text = "DT: " + mLCDate;

        //    DataRow[] DrSndrBank = mTbl01.Select("gencod='010100101005'");
        //    string mSndrBankNam = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["gendata"].ToString().Trim());
        //    string mSndrBankAdd = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["data1"].ToString().Trim());

        //    TextObject txtSenderBank = mRpt2.ReportDefinition.ReportObjects["txtSenderBank"] as TextObject;
        //    txtSenderBank.Text = mSndrBankNam;

        //    TextObject txtSenderBankAdd = mRpt2.ReportDefinition.ReportObjects["txtSenderBankAdd"] as TextObject;
        //    txtSenderBankAdd.Text = mSndrBankAdd;

        //    TextObject txtBLAWBNO = mRpt2.ReportDefinition.ReportObjects["txtBLAWBNO"] as TextObject;
        //    txtBLAWBNO.Text = mTbl03.Rows[0]["blawbno"].ToString();

        //    TextObject txtBLAWBDT = mRpt2.ReportDefinition.ReportObjects["txtBLAWBDT"] as TextObject;
        //    txtBLAWBDT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["blawbdt"]).ToString("dd-MMM-yyyy");

        //    TextObject txtINVRMRKS = mRpt2.ReportDefinition.ReportObjects["txtINVRMRKS"] as TextObject;
        //    txtINVRMRKS.Text = mTbl03.Rows[0]["invrmrks"].ToString();

        //    TextObject txtShipMark = mRpt2.ReportDefinition.ReportObjects["txtShipMark"] as TextObject;
        //    txtShipMark.Text = mTbl03.Rows[0]["shipmark"].ToString();

        //    TextObject txtCOMNAM = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM"] as TextObject;
        //    txtCOMNAM.Text = mTbl01.Rows[0]["comnam"].ToString();

        //    TextObject txtCOMADD = mRpt2.ReportDefinition.ReportObjects["txtCOMADD"] as TextObject;
        //    txtCOMADD.Text = mTbl01.Rows[0]["comadd1"].ToString().Trim() + ", " +
        //                     mTbl01.Rows[0]["comadd2"].ToString().Trim();

        //    DataRow[] DrBuyer = mTbl01.Select("gencod='010100101003'");
        //    string mBuyerNam = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["gendata"].ToString().Trim());
        //    string mBuyerAdd1 = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["data1"].ToString().Trim());

        //    TextObject txtBuyerName = mRpt2.ReportDefinition.ReportObjects["txtBuyerName"] as TextObject;
        //    txtBuyerName.Text = mBuyerNam;

        //    TextObject txtBuyerAdd1 = mRpt2.ReportDefinition.ReportObjects["txtBuyerAdd1"] as TextObject;
        //    txtBuyerAdd1.Text = mBuyerAdd1;


        //    DataRow[] DrNotify1 = mTbl01.Select("gencod='010100102003'");
        //    string mNotify1Name = (DrNotify1.Length == 0 ? "" : DrNotify1[0]["gendata"].ToString().Trim());
        //    string mNotify1Add = (DrNotify1.Length == 0 ? "" : DrNotify1[0]["data1"].ToString().Trim());
        //    TextObject txtNotifyParty1 = mRpt2.ReportDefinition.ReportObjects["txtNotifyParty1"] as TextObject;
        //    txtNotifyParty1.Text = mNotify1Name;
        //    TextObject txtNotifyPartyAdd1 = mRpt2.ReportDefinition.ReportObjects["txtNotifyPartyAdd1"] as TextObject;
        //    txtNotifyPartyAdd1.Text = mNotify1Add;

        //    DataRow[] DrNotify2 = mTbl01.Select("gencod='010100102005'");
        //    string mNotify2Name = (DrNotify2.Length == 0 ? "" : DrNotify2[0]["gendata"].ToString().Trim());
        //    string mNotify2Add = (DrNotify2.Length == 0 ? "" : DrNotify2[0]["data1"].ToString().Trim());
        //    TextObject txtNotifyParty2 = mRpt2.ReportDefinition.ReportObjects["txtNotifyParty2"] as TextObject;
        //    txtNotifyParty2.Text = mNotify2Name;
        //    TextObject txtNotifyPartyAdd2 = mRpt2.ReportDefinition.ReportObjects["txtNotifyPartyAdd2"] as TextObject;
        //    txtNotifyPartyAdd2.Text = mNotify2Add;

        //    DataRow[] Loading1 = mTbl01.Select("gencod='010100102007'");
        //    string mLoadingPort = (Loading1.Length == 0 ? "" : Loading1[0]["gendata"].ToString().Trim());

        //    TextObject txtPortLoading = mRpt2.ReportDefinition.ReportObjects["txtPortLoading"] as TextObject;
        //    txtPortLoading.Text = mLoadingPort;

        //    DataRow[] Dest1 = mTbl01.Select("gencod='010100102008'");
        //    string mDestPort = (Dest1.Length == 0 ? "" : Dest1[0]["gendata"].ToString().Trim());

        //    TextObject txtPortDest = mRpt2.ReportDefinition.ReportObjects["txtPortDest"] as TextObject;
        //    txtPortDest.Text = mDestPort;

        //    TextObject txtCONTNRNO = mRpt2.ReportDefinition.ReportObjects["txtCONTNRNO"] as TextObject;
        //    txtCONTNRNO.Text = mTbl03.Rows[0]["cntnrno"].ToString();

        //    TextObject txtMSURMNT = mRpt2.ReportDefinition.ReportObjects["txtMSURMNT"] as TextObject;
        //    txtMSURMNT.Text = "MEASUREMENT : " + mTbl03.Rows[0]["msurmnt"].ToString();

        //    TextObject txtTQTYDES = mRpt2.ReportDefinition.ReportObjects["txtTQTYDES"] as TextObject;
        //    txtTQTYDES.Text = "Total Quantity=" + mTbl03.Rows[0]["tqtydes"].ToString();

        //    TextObject txtTNWTDES = mRpt2.ReportDefinition.ReportObjects["txtTNWTDES"] as TextObject;
        //    txtTNWTDES.Text = "Total Net Weight=" + mTbl03.Rows[0]["tnwtdes"].ToString();

        //    TextObject txtTGWTDES = mRpt2.ReportDefinition.ReportObjects["txtTGWTDES"] as TextObject;
        //    txtTGWTDES.Text = "Total Gross Weight=" + mTbl03.Rows[0]["tgwtdes"].ToString();

        //    TextObject txtTCBMDES = mRpt2.ReportDefinition.ReportObjects["txtTCBMDES"] as TextObject;
        //    txtTCBMDES.Text = "Total CBM=" + mTbl03.Rows[0]["tcbmdes"].ToString();

        //    TextObject txtCOMNAM2 = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM2"] as TextObject;
        //    txtCOMNAM2.Text = mTbl01.Rows[0]["comnam"].ToString();
        //    TextObject txtuserinfo = mRpt2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    return mRpt2;
        //}

        //private ReportDocument CommercialInvoice()
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument mRpt2 = new RMGiRPT.R_03_CostABgd.rptComInvoice();

        //    DataTable mTbl01 = ((DataTable)Session["tblInvPrn01"]);
        //    DataTable mTbl03 = ((DataTable)Session["tblInvPrn03"]);
        //    TextObject txtINVNO2 = mRpt2.ReportDefinition.ReportObjects["txtINVNO2"] as TextObject;
        //    txtINVNO2.Text = mTbl03.Rows[0]["invno2"].ToString();

        //    TextObject txtSHIPMDAT = mRpt2.ReportDefinition.ReportObjects["txtSHIPMDAT"] as TextObject;
        //    txtSHIPMDAT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy");

        //    TextObject txtEXPORTNO = mRpt2.ReportDefinition.ReportObjects["txtEXPORTNO"] as TextObject;
        //    txtEXPORTNO.Text = mTbl03.Rows[0]["exportno"].ToString();

        //    TextObject txtEXPORTDT = mRpt2.ReportDefinition.ReportObjects["txtEXPORTDT"] as TextObject;
        //    txtEXPORTDT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["exportdt"]).ToString("dd-MMM-yyyy");

        //    TextObject txtMLCNo = mRpt2.ReportDefinition.ReportObjects["txtMLCNo"] as TextObject;
        //    txtMLCNo.Text = mTbl01.Rows[0]["mlcdes"].ToString().Trim();

        //    DataRow[] DrLCDate = mTbl01.Select("gencod='010100101001'");
        //    string mLCDate = Convert.ToDateTime(DrLCDate[0]["dateval"]).ToString("dd-MMM-yyyy");

        //    TextObject txtMLCDate = mRpt2.ReportDefinition.ReportObjects["txtMLCDate"] as TextObject;
        //    txtMLCDate.Text = "DT: " + mLCDate;

        //    DataRow[] DrSndrBank = mTbl01.Select("gencod='010100101005'");
        //    string mSndrBankNam = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["gendata"].ToString().Trim());
        //    string mSndrBankAdd = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["data1"].ToString().Trim());

        //    TextObject txtSenderBank = mRpt2.ReportDefinition.ReportObjects["txtSenderBank"] as TextObject;
        //    txtSenderBank.Text = mSndrBankNam;

        //    TextObject txtSenderBankAdd = mRpt2.ReportDefinition.ReportObjects["txtSenderBankAdd"] as TextObject;
        //    txtSenderBankAdd.Text = mSndrBankAdd;

        //    TextObject txtBLAWBNO = mRpt2.ReportDefinition.ReportObjects["txtBLAWBNO"] as TextObject;
        //    txtBLAWBNO.Text = mTbl03.Rows[0]["blawbno"].ToString();

        //    TextObject txtBLAWBDT = mRpt2.ReportDefinition.ReportObjects["txtBLAWBDT"] as TextObject;
        //    txtBLAWBDT.Text = "DT: " + Convert.ToDateTime(mTbl03.Rows[0]["blawbdt"]).ToString("dd-MMM-yyyy");

        //    TextObject txtINVRMRKS = mRpt2.ReportDefinition.ReportObjects["txtINVRMRKS"] as TextObject;
        //    txtINVRMRKS.Text = mTbl03.Rows[0]["invrmrks"].ToString();

        //    TextObject txtShipMark = mRpt2.ReportDefinition.ReportObjects["txtShipMark"] as TextObject;
        //    txtShipMark.Text = mTbl03.Rows[0]["shipmark"].ToString();

        //    TextObject txtCOMNAM = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM"] as TextObject;
        //    txtCOMNAM.Text = mTbl01.Rows[0]["comnam"].ToString();

        //    TextObject txtCOMADD = mRpt2.ReportDefinition.ReportObjects["txtCOMADD"] as TextObject;
        //    txtCOMADD.Text = mTbl01.Rows[0]["comadd1"].ToString().Trim() + ", " +
        //                     mTbl01.Rows[0]["comadd2"].ToString().Trim();

        //    DataRow[] DrBuyer = mTbl01.Select("gencod='010100101003'");
        //    string mBuyerNam = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["gendata"].ToString().Trim());
        //    string mBuyerAdd1 = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["data1"].ToString().Trim());

        //    TextObject txtBuyerName = mRpt2.ReportDefinition.ReportObjects["txtBuyerName"] as TextObject;
        //    txtBuyerName.Text = mBuyerNam;

        //    TextObject txtBuyerAdd1 = mRpt2.ReportDefinition.ReportObjects["txtBuyerAdd1"] as TextObject;
        //    txtBuyerAdd1.Text = mBuyerAdd1;

        //    DataRow[] DrNotify1 = mTbl01.Select("gencod='010100102003'");
        //    string mNotify1Name = (DrNotify1.Length == 0 ? "" : DrNotify1[0]["gendata"].ToString().Trim());
        //    string mNotify1Add = (DrNotify1.Length == 0 ? "" : DrNotify1[0]["data1"].ToString().Trim());
        //    TextObject txtNotifyParty1 = mRpt2.ReportDefinition.ReportObjects["txtNotifyParty1"] as TextObject;
        //    txtNotifyParty1.Text = mNotify1Name;
        //    TextObject txtNotifyPartyAdd1 = mRpt2.ReportDefinition.ReportObjects["txtNotifyPartyAdd1"] as TextObject;
        //    txtNotifyPartyAdd1.Text = mNotify1Add;

        //    DataRow[] DrNotify2 = mTbl01.Select("gencod='010100102005'");
        //    string mNotify2Name = (DrNotify2.Length == 0 ? "" : DrNotify2[0]["gendata"].ToString().Trim());
        //    string mNotify2Add = (DrNotify2.Length == 0 ? "" : DrNotify2[0]["data1"].ToString().Trim());
        //    TextObject txtNotifyParty2 = mRpt2.ReportDefinition.ReportObjects["txtNotifyParty2"] as TextObject;
        //    txtNotifyParty2.Text = mNotify2Name;
        //    TextObject txtNotifyPartyAdd2 = mRpt2.ReportDefinition.ReportObjects["txtNotifyPartyAdd2"] as TextObject;
        //    txtNotifyPartyAdd2.Text = mNotify2Add;

        //    DataRow[] Loading1 = mTbl01.Select("gencod='010100102007'");
        //    string mLoadingPort = (Loading1.Length == 0 ? "" : Loading1[0]["gendata"].ToString().Trim());

        //    TextObject txtPortLoading = mRpt2.ReportDefinition.ReportObjects["txtPortLoading"] as TextObject;
        //    txtPortLoading.Text = mLoadingPort;

        //    DataRow[] Dest1 = mTbl01.Select("gencod='010100102008'");
        //    string mDestPort = (Dest1.Length == 0 ? "" : Dest1[0]["gendata"].ToString().Trim());

        //    TextObject txtPortDest = mRpt2.ReportDefinition.ReportObjects["txtPortDest"] as TextObject;
        //    txtPortDest.Text = mDestPort;

        //    TextObject txtCONTNRNO = mRpt2.ReportDefinition.ReportObjects["txtCONTNRNO"] as TextObject;
        //    txtCONTNRNO.Text = mTbl03.Rows[0]["cntnrno"].ToString();

        //    TextObject txtORDERDES = mRpt2.ReportDefinition.ReportObjects["txtORDERDES"] as TextObject;
        //    txtORDERDES.Text = mTbl03.Rows[0]["orderdes"].ToString();

        //    string mStyleDes = mTbl03.Rows[0]["styldes"].ToString();

        //    for (int i = 1; i < mTbl03.Rows.Count; i++)
        //    {
        //        mStyleDes = mStyleDes + ", " + mTbl03.Rows[0]["styldes"].ToString();
        //    }

        //    TextObject txtSTYLEDES = mRpt2.ReportDefinition.ReportObjects["txtSTYLEDES"] as TextObject;
        //    txtSTYLEDES.Text = mStyleDes;

        //    double mLCQty = Convert.ToDouble((Convert.IsDBNull(mTbl03.Compute("sum(totqty)", "")) ?
        //        0.00 : mTbl03.Compute("sum(totqty)", "")));

        //    string mStyleUnit = mTbl03.Rows[0]["stylunit"].ToString();

        //    TextObject txtTOTQTY = mRpt2.ReportDefinition.ReportObjects["txtTOTQTY"] as TextObject;
        //    txtTOTQTY.Text = mLCQty.ToString("#,##0;(#,##0); ") + " " + mStyleUnit;

        //    double mLCamt = Convert.ToDouble((Convert.IsDBNull(mTbl03.Compute("sum(stylamt)", "")) ?
        //        0.00 : mTbl03.Compute("sum(stylamt)", "")));

        //    DataRow[] DrCurency = mTbl01.Select("gencod='010100101009'");
        //    string mCurency = (DrCurency.Length == 0 ? "" : DrCurency[0]["gendata"].ToString().Trim());

        //    TextObject txtShipmVal = mRpt2.ReportDefinition.ReportObjects["txtShipmVal"] as TextObject;
        //    txtShipmVal.Text = mCurency + " " + mLCamt.ToString("#,##0.00;(#,##0.00); ");

        //    TextObject txtShipmVal1 = mRpt2.ReportDefinition.ReportObjects["txtShipmVal1"] as TextObject;
        //    txtShipmVal1.Text = mCurency + " " + mLCamt.ToString("#,##0.00;(#,##0.00); ");

        //    TextObject txtRate = mRpt2.ReportDefinition.ReportObjects["txtRate"] as TextObject;
        //    txtRate.Text = mCurency + " " + Convert.ToDouble(mLCamt / mLCQty).ToString("#,##0.00;(#,##0.00); ") + "/" + mStyleUnit;

        //    DataRow[] DrByrCom = mTbl01.Select("gencod='010100101008'");
        //    double mByrCom = Convert.ToDouble("0" + (DrByrCom.Length == 0 ? "" :
        //                     DrByrCom[0]["gendata"].ToString().Trim().Replace("%", "")));

        //    TextObject txtByerComDes = mRpt2.ReportDefinition.ReportObjects["txtByerComDes"] as TextObject;
        //    txtByerComDes.Text = "AS PER L/C W+F COMMISSION " + mByrCom.ToString() + "%";

        //    TextObject txtByerComVal = mRpt2.ReportDefinition.ReportObjects["txtByerComVal"] as TextObject;
        //    txtByerComVal.Text = mCurency + " " + Convert.ToDouble(mLCamt * mByrCom / 100).ToString("#,##0.00;(#,##0.00); ");

        //    TextObject txtShipmVal2Des = mRpt2.ReportDefinition.ReportObjects["txtShipmVal2Des"] as TextObject;
        //    txtShipmVal2Des.Text = "TOTAL " + mLCQty.ToString("#,##0;(#,##0); ") + " " + mStyleUnit;

        //    TextObject txtShipmVal2 = mRpt2.ReportDefinition.ReportObjects["txtShipmVal2"] as TextObject;
        //    txtShipmVal2.Text = mCurency + " " + Convert.ToDouble(mLCamt - mLCamt * mByrCom / 100).ToString("#,##0.00;(#,##0.00); ");


        //    TextObject txtAmtInWord = mRpt2.ReportDefinition.ReportObjects["txtAmtInWord"] as TextObject;
        //    txtAmtInWord.Text = mCurency + " " + ASTUtility.Trans(Convert.ToDouble(mLCamt - mLCamt * mByrCom / 100), 4).ToUpper();

        //    double mCRTNQty = Convert.ToDouble((Convert.IsDBNull(mTbl03.Compute("sum(tcrtnqty)", "")) ?
        //            0.00 : mTbl03.Compute("sum(tcrtnqty)", "")));

        //    TextObject txtCRTNQTY = mRpt2.ReportDefinition.ReportObjects["txtCRTNQTY"] as TextObject;
        //    txtCRTNQTY.Text = mCRTNQty.ToString("#,##0;(#,##0); ") + " CTN";

        //    TextObject txtCOMNAM2 = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM2"] as TextObject;
        //    txtCOMNAM2.Text = mTbl01.Rows[0]["comnam"].ToString();
        //    TextObject txtuserinfo = mRpt2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


        //    return mRpt2;
        //}

        //private ReportDocument BillofExchange()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument mRpt2 = new RMGiRPT.R_03_CostABgd.rptBillofExchange();

        //    DataTable mTbl01 = ((DataTable)Session["tblInvPrn01"]);
        //    DataTable mTbl03 = ((DataTable)Session["tblInvPrn03"]);


        //    TextObject txtEXCHANGDAT = mRpt2.ReportDefinition.ReportObjects["txtEXCHANGDAT"] as TextObject;
        //    txtEXCHANGDAT.Text = Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy");


        //    double mLCamt = Convert.ToDouble((Convert.IsDBNull(mTbl03.Compute("sum(stylamt)", "")) ?
        //        0.00 : mTbl03.Compute("sum(stylamt)", "")));

        //    DataRow[] DrCurency = mTbl01.Select("gencod='010100101009'");
        //    string mCurency = (DrCurency.Length == 0 ? "" : DrCurency[0]["gendata"].ToString().Trim());

        //    TextObject txtShipmVal = mRpt2.ReportDefinition.ReportObjects["txtShipmVal"] as TextObject;
        //    txtShipmVal.Text = mCurency + " " + mLCamt.ToString("#,##0.00;(#,##0.00); ");

        //    TextObject txtAmtInWord = mRpt2.ReportDefinition.ReportObjects["txtAmtInWord"] as TextObject;
        //    txtAmtInWord.Text = mCurency + " " + ASTUtility.Trans(mLCamt, 4).ToUpper();

        //    DataRow[] DrRcvBank = mTbl01.Select("gencod='010100101006'");
        //    string mRcvBankNam = (DrRcvBank.Length == 0 ? "" : DrRcvBank[0]["gendata"].ToString().Trim());
        //    string mRcvBankAdd1 = (DrRcvBank.Length == 0 ? "" : DrRcvBank[0]["data1"].ToString().Trim());

        //    TextObject txtRcvBankName = mRpt2.ReportDefinition.ReportObjects["txtRcvBankName"] as TextObject;
        //    txtRcvBankName.Text = mRcvBankNam;

        //    TextObject txtRcvBankAdd1 = mRpt2.ReportDefinition.ReportObjects["txtRcvBankAdd1"] as TextObject;
        //    txtRcvBankAdd1.Text = mRcvBankAdd1;

        //    DataRow[] DrBuyer = mTbl01.Select("gencod='010100101003'");
        //    string mBuyerNam = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["gendata"].ToString().Trim());
        //    string mBuyerAdd1 = (DrBuyer.Length == 0 ? "" : DrBuyer[0]["data1"].ToString().Trim());

        //    TextObject txtBuyerName = mRpt2.ReportDefinition.ReportObjects["txtBuyerName"] as TextObject;
        //    txtBuyerName.Text = mBuyerNam;

        //    TextObject txtBuyerAdd1 = mRpt2.ReportDefinition.ReportObjects["txtBuyerAdd1"] as TextObject;
        //    txtBuyerAdd1.Text = mBuyerAdd1;

        //    DataRow[] DrSndrBank = mTbl01.Select("gencod='010100101005'");
        //    string mSndrBankNam = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["gendata"].ToString().Trim());
        //    string mSndrBankAdd1 = (DrSndrBank.Length == 0 ? "" : DrSndrBank[0]["data1"].ToString().Trim());

        //    TextObject txtSenderBank1 = mRpt2.ReportDefinition.ReportObjects["txtSenderBank1"] as TextObject;
        //    txtSenderBank1.Text = mSndrBankNam + ", " + mSndrBankAdd1;

        //    TextObject txtSenderBank2 = mRpt2.ReportDefinition.ReportObjects["txtSenderBank2"] as TextObject;
        //    txtSenderBank2.Text = mSndrBankNam + ", " + mSndrBankAdd1;

        //    TextObject txtMLCNo = mRpt2.ReportDefinition.ReportObjects["txtMLCNo"] as TextObject;
        //    txtMLCNo.Text = mTbl01.Rows[0]["mlcdes"].ToString().Trim();

        //    DataRow[] DrLCDate = mTbl01.Select("gencod='010100101001'");
        //    string mLCDate = (DrLCDate.Length == 0 ? "" : DrLCDate[0]["gendata"].ToString().Trim());
        //    TextObject txtMLCDate = mRpt2.ReportDefinition.ReportObjects["txtMLCDate"] as TextObject;
        //    txtMLCDate.Text = mLCDate;

        //    TextObject txtINVNO2 = mRpt2.ReportDefinition.ReportObjects["txtINVNO2"] as TextObject;
        //    txtINVNO2.Text = mTbl03.Rows[0]["invno2"].ToString();

        //    TextObject txtSHIPMDAT = mRpt2.ReportDefinition.ReportObjects["txtSHIPMDAT"] as TextObject;
        //    txtSHIPMDAT.Text = Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy");

        //    TextObject txtCOMNAM = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM"] as TextObject;
        //    txtCOMNAM.Text = mTbl01.Rows[0]["comnam"].ToString();
        //    TextObject txtuserinfo = mRpt2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    return mRpt2;
        //}

        //private ReportDocument ForwardingLetter()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument mRpt2 = new RMGiRPT.R_03_CostABgd.rptForwLetter();
        //    DataTable mTbl01 = ((DataTable)Session["tblInvPrn01"]);
        //    DataTable mTbl03 = ((DataTable)Session["tblInvPrn03"]);

        //    DataRow[] DrBank = mTbl01.Select("gencod='010100101006'");
        //    string mBankNam = (DrBank.Length == 0 ? "" : DrBank[0]["gendata"].ToString().Trim());
        //    string mBankAdd1 = (DrBank.Length == 0 ? "" : DrBank[0]["data1"].ToString().Trim());

        //    DataRow[] DrCurency = mTbl01.Select("gencod='010100101009'");
        //    string mCurency = (DrCurency.Length == 0 ? "" : DrCurency[0]["gendata"].ToString().Trim());

        //    TextObject txtBankName = mRpt2.ReportDefinition.ReportObjects["txtBankName"] as TextObject;
        //    txtBankName.Text = mBankNam;

        //    TextObject txtBankAdd1 = mRpt2.ReportDefinition.ReportObjects["txtBankAdd1"] as TextObject;
        //    txtBankAdd1.Text = mBankAdd1;

        //    double mLCamt = Convert.ToDouble((Convert.IsDBNull(mTbl03.Compute("sum(stylamt)", "")) ?
        //                0.00 : mTbl03.Compute("sum(stylamt)", "")));

        //    //this.gvSch.Columns[4].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01.Compute("sum(cinsam)", "")) ?
        //    //0.00 : tbl01.Compute("sum(cinsam)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    TextObject txtINVNO2 = mRpt2.ReportDefinition.ReportObjects["txtINVNO2"] as TextObject;
        //    txtINVNO2.Text = mTbl03.Rows[0]["invno2"].ToString();

        //    TextObject txtSHIPMDAT = mRpt2.ReportDefinition.ReportObjects["txtSHIPMDAT"] as TextObject;
        //    txtSHIPMDAT.Text = Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("MMMM dd, yyyy");
        //    TextObject txtSubject = mRpt2.ReportDefinition.ReportObjects["txtSubject"] as TextObject;
        //    txtSubject.Text = "Submission of Export Bill for " + mCurency + " " + mLCamt.ToString("#,##0.00;(#,##0.00); ") +
        //                      " against our Invoice No. " + mTbl03.Rows[0]["invno2"].ToString() +
        //                      " under export L/C No. " + mTbl01.Rows[0]["mlcdes"].ToString().Trim() + " dated " +
        //                      Convert.ToDateTime(mTbl03.Rows[0]["shipmdat"]).ToString("dd-MMM-yyyy") +
        //                      " for Negotiation.";
        //    TextObject txtPara1 = mRpt2.ReportDefinition.ReportObjects["txtPara1"] as TextObject;
        //    txtPara1.Text = "We are pleased to submit the following documents for " +
        //                    mCurency + " " + mLCamt.ToString("#,##0.00;(#,##0.00); ") +
        //                    " against our above export L/C. for Negotiation/Purchase purpose.";

        //    TextObject txtOriginal01 = mRpt2.ReportDefinition.ReportObjects["txtOriginal01"] as TextObject;
        //    txtOriginal01.Text = (this.rblFwl01.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal02 = mRpt2.ReportDefinition.ReportObjects["txtOriginal02"] as TextObject;
        //    txtOriginal02.Text = (this.rblFwl02.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal03 = mRpt2.ReportDefinition.ReportObjects["txtOriginal03"] as TextObject;
        //    txtOriginal03.Text = (this.rblFwl03.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal04 = mRpt2.ReportDefinition.ReportObjects["txtOriginal04"] as TextObject;
        //    txtOriginal04.Text = (this.rblFwl04.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal05 = mRpt2.ReportDefinition.ReportObjects["txtOriginal05"] as TextObject;
        //    txtOriginal05.Text = (this.rblFwl05.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal06 = mRpt2.ReportDefinition.ReportObjects["txtOriginal06"] as TextObject;
        //    txtOriginal06.Text = (this.rblFwl06.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal07 = mRpt2.ReportDefinition.ReportObjects["txtOriginal07"] as TextObject;
        //    txtOriginal07.Text = (this.rblFwl07.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal08 = mRpt2.ReportDefinition.ReportObjects["txtOriginal08"] as TextObject;
        //    txtOriginal08.Text = (this.rblFwl08.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal09 = mRpt2.ReportDefinition.ReportObjects["txtOriginal09"] as TextObject;
        //    txtOriginal09.Text = (this.rblFwl01.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtOriginal10 = mRpt2.ReportDefinition.ReportObjects["txtOriginal10"] as TextObject;
        //    txtOriginal10.Text = (this.rblFwl10.SelectedIndex == 1 ? "X" : "");

        //    TextObject txtCopy01 = mRpt2.ReportDefinition.ReportObjects["txtCopy01"] as TextObject;
        //    txtCopy01.Text = (this.rblFwl01.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy02 = mRpt2.ReportDefinition.ReportObjects["txtCopy02"] as TextObject;
        //    txtCopy02.Text = (this.rblFwl02.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy03 = mRpt2.ReportDefinition.ReportObjects["txtCopy03"] as TextObject;
        //    txtCopy03.Text = (this.rblFwl03.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy04 = mRpt2.ReportDefinition.ReportObjects["txtCopy04"] as TextObject;
        //    txtCopy04.Text = (this.rblFwl04.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy05 = mRpt2.ReportDefinition.ReportObjects["txtCopy05"] as TextObject;
        //    txtCopy05.Text = (this.rblFwl05.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy06 = mRpt2.ReportDefinition.ReportObjects["txtCopy06"] as TextObject;
        //    txtCopy06.Text = (this.rblFwl06.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy07 = mRpt2.ReportDefinition.ReportObjects["txtCopy07"] as TextObject;
        //    txtCopy07.Text = (this.rblFwl07.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy08 = mRpt2.ReportDefinition.ReportObjects["txtCopy08"] as TextObject;
        //    txtCopy08.Text = (this.rblFwl08.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy09 = mRpt2.ReportDefinition.ReportObjects["txtCopy09"] as TextObject;
        //    txtCopy09.Text = (this.rblFwl09.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCopy10 = mRpt2.ReportDefinition.ReportObjects["txtCopy10"] as TextObject;
        //    txtCopy10.Text = (this.rblFwl10.SelectedIndex == 2 ? "X" : "");

        //    TextObject txtCOMNAM = mRpt2.ReportDefinition.ReportObjects["txtCOMNAM"] as TextObject;
        //    txtCOMNAM.Text = mTbl01.Rows[0]["comnam"].ToString();
        //    TextObject txtuserinfo = mRpt2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    return mRpt2;
        //}



        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable tbl01 = ((DataTable)Session["tblShip"]);
            for (int i = 0; i < tbl01.Rows.Count; i++)
            {
                double mF7201001 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201001")).Text.Trim());
                double mF7201002 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201002")).Text.Trim());
                double mF7201003 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201003")).Text.Trim());
                double mF7201004 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201004")).Text.Trim());
                double mF7201005 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201005")).Text.Trim());
                double mF7201006 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201006")).Text.Trim());
                double mF7201007 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201007")).Text.Trim());
                double mF7201008 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201008")).Text.Trim());
                double mF7201009 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201009")).Text.Trim());
                double mF7201010 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201010")).Text.Trim());
                double mF7201011 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201011")).Text.Trim());
                double mF7201012 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201012")).Text.Trim());
                double mF7201013 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201013")).Text.Trim());
                double mF7201014 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201014")).Text.Trim());
                double mF7201015 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201015")).Text.Trim());
                double mCrtnqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvCrtnqty")).Text.Trim());
                string mCrtnslno = ((TextBox)gv1.Rows[i].FindControl("txtgvCrtnslno")).Text.Trim();


                tbl01.Rows[i]["F7201001"] = mF7201001;
                tbl01.Rows[i]["F7201002"] = mF7201002;
                tbl01.Rows[i]["F7201003"] = mF7201003;
                tbl01.Rows[i]["F7201004"] = mF7201004;
                tbl01.Rows[i]["F7201005"] = mF7201005;
                tbl01.Rows[i]["F7201006"] = mF7201006;
                tbl01.Rows[i]["F7201007"] = mF7201007;
                tbl01.Rows[i]["F7201008"] = mF7201008;
                tbl01.Rows[i]["F7201009"] = mF7201009;
                tbl01.Rows[i]["F7201010"] = mF7201010;
                tbl01.Rows[i]["F7201011"] = mF7201011;
                tbl01.Rows[i]["F7201012"] = mF7201012;
                tbl01.Rows[i]["F7201013"] = mF7201013;
                tbl01.Rows[i]["F7201014"] = mF7201014;
                tbl01.Rows[i]["F7201015"] = mF7201015;
                tbl01.Rows[i]["Total1"] = mF7201001 + mF7201002 + mF7201003 + mF7201004 + mF7201005 +
                                                    mF7201006 + mF7201007 + mF7201008 + mF7201009 + mF7201010 +
                                                    mF7201011 + mF7201012 + mF7201013 + mF7201014 + mF7201015;
                tbl01.Rows[i]["crtnqty"] = mCrtnqty;
                tbl01.Rows[i]["crtnslno"] = mCrtnslno;
            }
            this.gv1_CalculateFootertotal(gv1, tbl01);

            //if (Convert.ToDouble(tbl01.Rows[TblRowIndex]["Total1"]) > 0)
            //{
            //    string mStyleID = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvStyleID")).Text.Trim();
            //    string mColorID = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvColorID")).Text.Trim();
            //    string mUnit1 = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvUnit1")).Text.Trim();
            //    string mCntrGrp = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvSLNO")).Text.Trim().Substring(0, 2);
            //    string mNewRowSl = Convert.ToInt32(Convert.ToInt32(mCntrGrp) + 1).ToString("00");
            //    bool mRowFound = false;
            //    for (int i = 0; i < tbl01.Rows.Count; i++)
            //    {
            //        string mStyleID1 = tbl01.Rows[i]["styleid"].ToString().Trim();
            //        string mColorID1 = tbl01.Rows[i]["colorid"].ToString().Trim();
            //        string mCntrGrp1 = tbl01.Rows[i]["crtngrp"].ToString().Trim();
            //        if (mStyleID + mColorID + mNewRowSl == mStyleID1 + mColorID1 + mCntrGrp1)
            //        {
            //            mRowFound = true;
            //            break;
            //        }
            //    }
            //    if (!mRowFound)
            //    {
            //        DataRow dr1 = tbl01.NewRow();
            //        dr1["styleid"] = mStyleID;
            //        dr1["colorid"] = mColorID;
            //        dr1["StyleDes"] = ">>";
            //        dr1["Desc1"] = ">";
            //        dr1["Unit1"] = mUnit1;
            //        dr1["crtngrp"] = mNewRowSl;
            //        dr1["F7201001"] = 0; dr1["F7201002"] = 0; dr1["F7201003"] = 0; dr1["F7201004"] = 0;
            //        dr1["F7201005"] = 0; dr1["F7201006"] = 0; dr1["F7201007"] = 0; dr1["F7201008"] = 0;
            //        dr1["F7201009"] = 0; dr1["F7201010"] = 0; dr1["F7201011"] = 0; dr1["F7201012"] = 0;
            //        dr1["F7201013"] = 0; dr1["F7201014"] = 0; dr1["F7201015"] = 0; dr1["Total1"] = 0;
            //        dr1["crtnqty"] = 0;
            //        dr1["crtnslno"] = "";
            //        tbl01.Rows.Add(dr1);
            //    }
            //}
            DataView ddv1 = tbl01.DefaultView;
            ddv1.Sort = "styleid, colorid, crtngrp";
            Session["tblShip"] = ddv1.ToTable();
            this.gv1.DataSource = ((DataTable)Session["tblShip"]);
            this.gv1.DataBind();
            this.PanelLC2.Visible = true;

        }
        //protected void DDLDocName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.lbljavascript.Text = "";

        //    if (this.DDLDocName.SelectedIndex == 0)
        //    {
        //        this.PanelFLetter.Visible = false;

        //    }
        //    else if (this.DDLDocName.SelectedIndex == 1)
        //    {
        //        this.PanelFLetter.Visible = true;

        //    }
        //    else if (this.DDLDocName.SelectedIndex == 2)
        //    {
        //        this.PanelFLetter.Visible = false;


        //    }
        //    else
        //    {
        //        this.PanelFLetter.Visible = false;

        //    }

        //}

        protected void DDLOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.shipmentType();
        }
    }
}