<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccConversion.aspx.cs" Inherits="SPEWEB.F_34_Mgt.AccConversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                     <asp:HiddenField ID="hfBaseCurrency" runat="server" Value="" />
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrm" runat="server" CssClass="label">From</asp:Label>

                                <asp:DropDownList ID="ddlfromcurrency" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">To</asp:Label>

                                <asp:DropDownList ID="ddltocurrency" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Conversion</asp:Label>
                                <asp:TextBox ID="txtConversion" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm small"
                                    OnClick="lbtnSelect_Click">Select</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height:300px;">

                    <div class="row">
                        <asp:GridView ID="gvConversion" runat="server" AutoGenerateColumns="False" PageSize="15" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfromcurrency" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcodedesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtocurrency" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tcodedesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="1 SGD = FC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconrate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="1 FC = SGD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvconrate1" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate1")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gvPagination" />
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

