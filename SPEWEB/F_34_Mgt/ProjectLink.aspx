<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProjectLink.aspx.cs" Inherits="SPEWEB.F_34_Mgt.ProjectLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        });

        function pageLoaded() {
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
                <div class="card-body" style="min-height: 350px;">
                    <div class="row" runat="server" visible="false" id="Panel2">
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="label" OnClick="ImgbtnFindProject_Click">Store Code</asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
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
                        <asp:GridView ID="gvProLinkInfo" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
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
                                <asp:TemplateField HeaderText="user Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod0" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" />

                                <asp:TemplateField HeaderText="pactcode Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprocode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvproDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSuplUpdate" runat="server" OnClick="lbtnSuplUpdate_Click"
                                            CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                    </FooterTemplate>
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





                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

