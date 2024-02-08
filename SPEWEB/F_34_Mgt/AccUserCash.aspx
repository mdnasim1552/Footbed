<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccUserCash.aspx.cs" Inherits="SPEWEB.F_34_Mgt.AccUserCash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">User Name</asp:Label>
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 350px;">
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <div class="row">

                                <div class="col-md-6 col-sm-6 col-lg-6 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="label" OnClick="ImgbtnFindProject_Click">Control Code</asp:LinkButton>
                                        <asp:DropDownList ID="ddlConTrolCode" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectSupl1_Click">Add</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectAll_Click">Add All</asp:LinkButton>

                                    </div>
                                </div>


                            </div>
                            <div class="row">

                                <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                    OnRowDeleting="gvProLinkInfo_RowDeleting">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                        <asp:TemplateField HeaderText="bactcode Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                         <%--   <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                    Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                    Style="text-align: center;" Width="90px">Delete All</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                         <%--   <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                    Font-Size="13px" OnClick="lbtnUpdate_Click"
                                                    Style="text-align: center; height: 15px;" Width="90px">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

