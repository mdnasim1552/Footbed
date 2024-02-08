<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptCriticalorder.aspx.cs" Inherits="SPEWEB.F_05_ProShip.RptCriticalorder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="~/Scripts/ScrollableTablePlugin.js"></script>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            var gvCrticalOrder = $('#<%=this.gvCrticalOrder.ClientID %>');
            gvCrticalOrder.Scrollable();

        }

    </script>

    <style>
        .GridCellDiv {
            width: 75px !important;
        }

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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1 form-group">
                                <asp:Label ID="LblSeason" runat="server" Text="Season" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlSeason" runat="server" CssClass="form-control form-control-sm chzn-select" ></asp:DropDownList>
                                </div>
                        </div>
                        <div class="col-md-2 form-group">
                                <asp:Label ID="lblbuyer" runat="server" CssClass="label">Buyer</asp:Label>
                                <asp:DropDownList ID="DdlBuyer" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" ></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 form-group">
                            <asp:Label ID="lblPage" runat="server" class="control-label" for="ddlpagesize">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="500">500</asp:ListItem>
                                <asp:ListItem Value="1000">1000</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">

                    <div class="table-responsive">
                        <asp:GridView ID="gvCrticalOrder" runat="server" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLSlNo" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcol1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col1")) %>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcol2" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col2")) %>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

