<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AnalysisMatList.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.AnalysisMatList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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

                        <%--<div class="col-md-2 form-group" >
                            <asp:Label runat="server" ID="Label2">Material Group</asp:Label>
                            <asp:DropDownList ID="ddlMatGroup" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlMatGroup_SelectedIndexChanged"></asp:DropDownList>
                        </div>--%>

                        <div class="col-md-2 form-group">
                            <asp:Label runat="server" ID="Label1">Material Subgroup</asp:Label>
                            <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>

                        <div class="col-md-1 form-group" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn-sm btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-2 form-group" style="margin-top: 20px;">
                            <asp:HyperLink ID="lbtnNewAnl" runat="server" CssClass="btn-sm btn btn-outline-info" Text="New Analysis" NavigateUrl="~/F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode=" Target="_blank"></asp:HyperLink>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">

                    <div class="row">

                        <asp:GridView ID="gvAnlMatList" runat="server" AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True"
                             CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Bottom" />
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsl3" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Material Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatName3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "materialsname")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="220px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnt3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpcf3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="380px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Convertible">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConv3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isconvrtble")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="75px" HorizontalAlign="center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItmCt3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="75px" HorizontalAlign="center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="STD. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cons. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStdQty3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="80px" HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <span class="fa fa-check btn-outline-primary"></span> 
                                    </HeaderTemplate> 
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkAnalysis" runat="server" Text="Analysis" NavigateUrl='<%# Eval("prodcode", "~/F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode={0}") %>' Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
