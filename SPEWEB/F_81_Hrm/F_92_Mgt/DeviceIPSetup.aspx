<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DeviceIPSetup.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.DeviceIPSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

        };

        function loadModalAddCode() {
            $('#AddIpAdd').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };

        function CloseModalAddCode() {
            $('#AddIpAdd').modal('hide');
        };
    </script>
    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card mt-2">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-1" style="margin-right: 280px">
                            <asp:LinkButton ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-plus" aria-hidden="true"></i> Add New IP</asp:LinkButton>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="display: flex">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label mr-2 mt-1" Text="Size:"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" AutoPostBack="True" Width="71px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-11 table-responsive" style="min-height: 500px;">
                            <asp:GridView runat="server" ID="grvIpSetup" AllowPaging="True" CssClass="table-striped  table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="grvacc_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DeleteBtn"  Style="color: red;" runat="server" OnClick="DeleteBtn_Click" OnClientClick="return FunConfirm();"><i class="fa fa-trash" ></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Machine No">

                                        <ItemTemplate>

                                            <asp:label ID="txtMachNo" runat="server" CssClass="border-0" Wrap="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machno")) %>'>
                                            </asp:label>

                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ip Address">
                                        <ItemTemplate>

                                            <asp:label ID="txtIpAddress" runat="server" CssClass="border-0" Wrap="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ipaddress")) %>'>
                                            </asp:label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="95px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alias">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="border-0" Wrap="true"
                                                Style="width: 80px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machinealias")) %>'>>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Port">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPort" runat="server" CssClass="border-0"
                                                Style="width: 75px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "port")) %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Corcernname">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCorcernname" runat="server" CssClass="border-0"
                                                Style="width: 100px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "corcernname")) %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Comcod" Visible="false">
                                        <ItemTemplate>
                                            <asp:label ID="lvlComcod" runat="server" CssClass="border-0"
                                                Style="width: 75px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'>
                                            </asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRowsNew" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


            <div id="AddIpAdd" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New IP Address</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <label class="col-md-3">Machine No</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtMachineNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">IP Address</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtIpAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">Alias</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">Port</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtPort" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" ToolTip="Update Ip Info." OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click">
                                <i class="fas fa-plus"></i>&nbsp;Add</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
