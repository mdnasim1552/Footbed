<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LCOpening.aspx.cs" Inherits="SPEWEB.F_09_Commer.LCOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="dchk1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function OpenMatlistmodal() {

            $('#matlistmodal').modal('show');
        }

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "dgvOrder":
                    tblData = document.getElementById("<%=dgvOrder.ClientID %>");
                    break;
            }

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function SelectAllCheckboxes(chk) {
            $('#<%=gvMatlist.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false) {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
            });
        }
        function openModal() {
            $('#LcFiledValue').modal('toggle');
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#Buyer').modal('hide');
            $('#LcFiledValue').modal('hide');
            $('#SpecificationModal').modal('hide');
            $('#matlistmodal').modal('hide');
        }

        function SpcfChangModal() {
            $('#SpecificationModal').modal('toggle');
        }



        function pageLoaded() {
            var dgvOrder = $('#<%=this.dgvOrder.ClientID %>');
            dgvOrder.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

        };

    </script>
    <style>
        #matlistmodal .modal-dialog {
            max-width: 100% !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-3 ">
                            <div class="form-group">
                                <asp:Label ID="lblLcno" runat="server" CssClass="label">L/C Number</asp:Label>

                                <asp:DropDownList ID="ddlLcCode" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group form-inline" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkOpen" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOpen_Click">Ok</asp:LinkButton>
                                &nbsp; <a class="btn btn-success btn-xs" href="#Buyer" data-toggle="modal"><i class="fa fa-plus"></i></a>
                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <span id="Lblfrom" visible="false" runat="server" class="lblTxt">From</span>
                                <asp:DropDownList ID="ddlFromCopy" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="LbtnCopy" OnClick="LbtnCopy_Click" OnClientClick="return confirm('Do you want to copy Genereal information from this LC?');" runat="server" CssClass="btn btn-xs btn-warning" Visible="false">Copy</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LbSupp" runat="server" Visible="false" class="label">Select Supplier</asp:Label>
                                <asp:DropDownList ID="ddlSupplier" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnReq" Visible="false" runat="server" CssClass="btn btn-success btn-xs" OnClick="lbtnReq_Click">PO/Req Link</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:CheckBox ID="gvCheckbx" runat="server" AutoPostBack="true" class="label" Text="Add New PO" OnCheckedChanged="gvCheckbx_CheckedChanged" />
                                <asp:DropDownList ID="ddlPoNo" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <asp:TextBox ID="txtconv" runat="server" Visible="false"></asp:TextBox>
                        <div class="col-md-4">
                            <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea mygvinputbox">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnFiledvalueentru" runat="server" OnClick="btnFiledvalueentru_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:LinkButton>

                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server" Style="font-size: 9px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="100px"></asp:Label>

                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="2px"></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:HyperLink ID="HypLcPreCosting" Target="_blank" CssClass="btn btn-xs btn-warning"
                                                    NavigateUrl='<%# this.ResolveUrl("~/F_09_Commer/RptLCStatus?Type=LCCostingPreset&actcode="+this.ddlLcCode.SelectedValue.ToString()) %>' runat="server"><span class="fa fa-hand-pointer"></span> Propose Costing</asp:HyperLink>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnSaveCust" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lnkbtnSaveCust_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="220px"></asp:TextBox>
                                                <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="220px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                                <asp:Panel ID="PanelOther" runat="server">

                                                    <div class="form-group" style="margin-bottom: 0px;">

                                                        <asp:DropDownList ID="ddlAlType" runat="server" CssClass=" form-control chzn-select" AutoPostBack="true" TabIndex="2">
                                                        </asp:DropDownList>

                                                    </div>


                                                </asp:Panel>

                                                <asp:Panel ID="pnlcurrency" runat="server">

                                                    <div class="form-group" style="margin-bottom: 0px;">

                                                        <asp:DropDownList ID="ddlcurrency" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlcurrency_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>


                                                </asp:Panel>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                </br></br></br></br>
                            </asp:Panel>
                        </div>
                        <div class="col-md-8">

                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewOrder" runat="server">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="lblproduct" runat="server" CssClass="label">Resource:</asp:Label>

                                                <asp:DropDownList ID="ddlResList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" CssClass="label">Specification:</asp:Label>


                                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-2 ">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:LinkButton ID="lnkAddTable" Style="margin-left: 15px;" runat="server" CssClass="btn btn-primary  btn-xs " OnClick="lnkAddTable_Click">Add <span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                            </div>

                                        </div>

                                    </div>


                                    <div class="table-responsive">
                                        <asp:GridView ID="dgvOrder" runat="server" OnRowDeleting="dgvOrder_RowDeleting" AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea">


                                            <Columns>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                            Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCode" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                        <asp:Label ID="lblBomid" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                        <asp:Label ID="lblgvssircode" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'></asp:Label>
                                                        <asp:Label ID="lblgvsyspon" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "syspon")) %>'></asp:Label>
                                                        <asp:Label ID="lblgvreqno" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkpsame" Style="font-size: 10px;" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lnkSameValue_Click">Put Same</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <HeaderStyle Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvscode" runat="server" Font-Size="9px" Width="40px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Resource Description" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Resource" Width="100"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-success" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary   btn-sm" OnClick="lnkTotal_Click">Total Calc</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvResdesc" runat="server" Width="120px" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" Width="200" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgrvspc" OnClick="lblgvSpfDesc10_Click" Width="120px" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchPO" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="PO No." onkeyup="Search_Gridview(this,4, 'dgvOrder')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvpono" runat="server" Width="60px" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Req NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvReqNo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BOM NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvspccode" Visible="false" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                        <asp:Label ID="lblgvBOmid" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvUnit" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgrvFOrderQty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066" Width="50px"
                                                            Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgrvOrderQty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="10px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <FooterStyle Font-Bold="true" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Free Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgrvFreeqty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                            BorderStyle="solid" BorderColor="#ef5b5b" BorderWidth="1px" Font-Size="10px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgrvFFreeqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FC Rate">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" OnClick="lnkFinalUpdate_Click" CssClass="btn btn-danger btn-sm">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgrvRate" runat="server" BackColor="Transparent" BorderStyle="solid" BorderColor="#ef5b5b" BorderWidth="1px"
                                                            Font-Size="10px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" FC Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgrvFamount" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvamount" runat="server" Font-Size="10px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BDT Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgrvFBDTamount" runat="server" Font-Bold="True" Font-Size="10px"
                                                            ForeColor="#CC0066" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrvBDTamount" runat="server" Font-Size="10px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:CommandField ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" HeaderText="" ItemStyle-Font-Size="10px"
                                                    ShowDeleteButton="True">
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:CommandField>
                                            </Columns>

                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                        <br />
                                        <div class="label label-success" id="lblcharging" runat="server" visible="false" style="font-size: 14px"><big>Charging Information</big></div>

                                        <asp:GridView ID="gvcharging" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="true">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CHlblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right" ToolTip="Click for Details Input"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpono" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                            Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupanme" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                            Width="270px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Charging">

                                                    <ItemTemplate>
                                                        <asp:Label ID="ChtxtgvStyle" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                            Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChrAmount" runat="server" Style="text-align: right" Font-Size="11px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFchamt" runat="server" Style="text-align: right;"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                            </Columns>

                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <FooterStyle CssClass="grvFooter" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />

                                        </asp:GridView>
                                    </div>


                                </asp:View>

                            </asp:MultiView>


                        </div>
                    </div>




                </div>

            </div>



            <div class="modal fade" id="Buyer" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header modal-header-success">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h5><i class="glyphicon glyphicon-thumbs-up"></i>Add New LC</h5>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="form-group">

                                <asp:DropDownList runat="server" ID="ddlLcCategoris" CssClass="form-control">
                                </asp:DropDownList>

                                <hr />

                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TxtLCnumber" runat="server" placeholder="L/C Number" Rows="7" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="SaveLC" CssClass="btn btn-success" runat="server" OnClick="Save_Click" OnClientClick="CLoseMOdal();">Save</asp:LinkButton>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->

            <!-- /.modal -->

            <div class="modal fade" id="LcFiledValue" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header modal-header-success">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h5><i class="glyphicon glyphicon-thumbs-up"></i>Add New
                                <asp:Label ID="lcFiled" runat="server"></asp:Label></h5>
                            <asp:Label ID="lblgcod" runat="server"></asp:Label>
                        </div>
                        <div class="modal-body form-horizontal">

                            <div class="form-group">
                                <asp:TextBox ID="TextBox2" runat="server" placeholder="L/C Number" Rows="7" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success" runat="server" OnClick="LinkButton1_Click" OnClientClick="CLoseMOdal();">Save</asp:LinkButton>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->

            <!------------specification change model------------->
            <div id="SpecificationModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title"><span class="fa fa-table"></span>
                                <asp:Label ID="ModalHead" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
                            <div class="form-group">
                                <label class="col-md-4">Specifications</label>
                                <div class="col-md-7">
                                    <asp:DropDownList ID="ddlSpecification" CssClass="form-control" runat="server">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-1">
                                    <a class="btn btn-success btn-xs" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                                        <span class="glyphicon glyphicon-plus-sign"></span>
                                    </a>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="collapse" id="collapseExample">
                                    <div class="card card-body">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-md-4">Thikness/Size</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="TxtThikness" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4">Width/Length</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtlength" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4">Color</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlModalColor" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4">Brand</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4">Remarks</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LbtnChngSpcf" runat="server" OnClick="LbtnChngSpcf_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!---------material list modal------------>
            <div id="matlistmodal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 class="modal-title"><span class="fa fa-table"></span>Details Information </h4>
                        </div>
                        <div class="modal-body form-horizontal">

                            <asp:GridView ID="gvMatlist" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Visible="False" />

                                <Columns>

                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reqno">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqno" runat="server" Font-Size="9px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPono" runat="server" Font-Size="9px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                Width="90px"></asp:Label>
                                            <asp:Label ID="lblgvSysPono" Visible="false" runat="server" Font-Size="9px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "syspon")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Size="9px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOM NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorderno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Req Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRecvqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRate" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOrderQty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bal Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalQty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ChkAll" ClientIDMode="Static" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkack" ClientIDMode="Static" runat="server" CssClass="chkack"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? true : false %>'
                                                Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? false : true%>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier As CS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSupDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cssupplierdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblssircode" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="80px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="ModalUpdateBtnLink" OnClick="ModalUpdateBtnLink_Click" OnClientClick="CLoseMOdal();"
                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
