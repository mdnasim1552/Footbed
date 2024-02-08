<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DelvChallan.aspx.cs" Inherits="SPEWEB.F_19_EXP.DelvChallan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function openModal() {
            $('#myModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-container{
            width: 100% !important;
        }

        .chzn-drop{
            width: 100% !important;
        }

        .chzn-search input{
            width: 100% !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <asp:Label ID="slrowid"  runat="server" Visible="false" >1</asp:Label>

            <div class="card card-fluid mb-1">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">

                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server" CssClass="label">CHL No</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" Text="DCH00-" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:Label>
                                    <asp:TextBox ID="txtCurNo2" Text="00000" runat="server" ReadOnly="True" Style="width: 50%" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label18" runat="server" CssClass="label">Ref No</asp:Label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Dispatch Mode</asp:Label>
                                <asp:DropDownList ID="DDLDespatch" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="row">
                                <div class="col-md-10 col-sm-10 col-lg-10 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblBUyer" runat="server" CssClass="label">Buyer Name</asp:Label>
                                        <asp:DropDownList ID="ddlBuyer" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="imgPreVious" runat="server" CssClass="control-label" OnClick="imgPreVious_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-lg-12 ">
                            <asp:Panel ID="plnexport" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="label">Invoice No</asp:Label>
                                            <asp:DropDownList ID="ddlprocode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprocode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Po List</asp:Label>
                                            <asp:DropDownList ID="DdlPoList" AutoPostBack="true" OnSelectedIndexChanged="DdlPoList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-lg-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">Product Details</asp:Label>
                                            <asp:DropDownList ID="DdlProduct" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlProduct_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" CssClass="label">Location</asp:Label>
                                            <asp:DropDownList ID="DdlLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:LinkButton ID="Add" runat="server" CssClass="btn btn-primary btn-sm " OnClick="Add_Click" ToolTip="Add Item"><span class="fa fa-check"></span></asp:LinkButton>
                                            <asp:LinkButton ID="AddAll" runat="server" CssClass="btn btn-primary btn-sm " Visible="false" OnClick="AddAll_Click" ToolTip="Add All Item"><span class="fa fa-check-double"></span></asp:LinkButton>
                                            <asp:LinkButton ID="RefBtn" runat="server" CssClass="btn btn-primary btn-sm  btn-danger" OnClick="RefBtn_Click" ToolTip="Refresh"><span class="fa fa-sync"></span></asp:LinkButton>
                                            <asp:LinkButton ID="LbtnAddMoreInv" runat="server" CssClass="btn btn-primary btn-sm pull-left btn-warning" OnClick="LbtnAddMoreInv_Click" ToolTip="Stock Reload"><span class="fa fa-shopping-basket"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 px-0">
                                        <div class="form-group">
                                            <asp:Label ID="lblPrntType" runat="server" CssClass="font-weight-bold" Text="Print Type"></asp:Label>
                                            <asp:DropDownList ID="ddlPrntType" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="1" Text="Format 1"></asp:ListItem>
                                                <asp:ListItem Selected="True" Value="2" Text="Format 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>

                </div>
            </div>








            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <asp:GridView ID="gvSalCon" OnRowDataBound="gvSalCon_RowDataBound" runat="server" ShowFooter="true" OnRowDeleting="gvSalCon_RowDeleting" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="15px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv No">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvInvno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO No">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvpono" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "po")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvMlcdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Style">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvstyle" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                        Width="100px"></asp:Label>
                                    <asp:Label ID="LblStyleid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'></asp:Label>
                                    <asp:Label ID="LblMlccod" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>
                                    <asp:Label ID="LblDayid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdayid")) %>'></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Color">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvColor" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                        Width="90px"></asp:Label>
                                    <asp:Label ID="LblgvColorid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                        Width="90px"></asp:Label>
                                    <asp:Label ID="LblGvSizeid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="txtFTotal" runat="server" ForeColor="Black" Text="Total"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wearhouse">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvwearhouseno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wearhouse")) %>'
                                        Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbgvsize" runat="server" OnClick="lbgvsize_Click" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                        Width="60px"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="INV Qty">
                                <FooterTemplate>
                                    <asp:Label ID="txtFInvqty" runat="server" ForeColor="Black"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblgvInvqty" runat="server"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bal <br> PRS">
                                <FooterTemplate>
                                    <asp:Label ID="txtFtotlprs" runat="server" ForeColor="Black"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtotlprs" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bal <br> CTNS">
                                <FooterTemplate>
                                    <asp:Label ID="txtFtotlctn" runat="server" ForeColor="Black"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtotlctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvStock" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvLocation" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locdesc")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                        </Columns>

                        <AlternatingRowStyle />
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-3" style="min-height: 300px;">

                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" CssClass="label">Vehicle No</asp:Label>
                                    <asp:TextBox ID="txtVan" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>


                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="label">Contact Person</asp:Label>
                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblContainer" runat="server" CssClass="label">Container No</asp:Label>
                                    <asp:TextBox ID="txtCntnrNo" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-3" style="min-height: 300px;">


                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="label">Mobile</asp:Label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="label">Seal No</asp:Label>
                                    <asp:TextBox ID="TxtSealNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>

                              

                                <div class="form-group">
                                    <asp:Label ID="lblLicnseNo" runat="server" CssClass="label">License/ID</asp:Label>
                                    <asp:TextBox ID="txtLicnseNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>

                            </div>
                             <div class="col-md-2 col-sm-2 col-lg-3" style="min-height: 300px;">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="label">Transport</asp:Label>
                                     <asp:DropDownList ID="ddltrans" AutoPostBack="true" OnSelectedIndexChanged="DdlPoList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                   
                                </div>
                                    <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="label">Port of Discharge</asp:Label>
                                    <asp:TextBox ID="txtpod" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-2 col-sm-2 col-lg-3" style="min-height: 300px;">

                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="label">Narration</asp:Label>
                                    <asp:TextBox ID="txtBillNarr" runat="server" CssClass="form-control " TextMode="MultiLine" cols="20" Rows="4"></asp:TextBox>
                                </div>

                            </div>

                        </div>


                    </asp:Panel>
                </div>
            </div>



            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Article and Order Wise Stock formation </h4>
                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>

                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12 table-responsive">
                                    <asp:GridView ID="gvStockDetails" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlblgvSlNo0" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblResDesc" runat="server"
                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="600px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblLocation" runat="server"
                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locationdesc")) %>'
                                                        Width="80px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlgvqtyStock" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="DlgvproFqty" runat="server"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
