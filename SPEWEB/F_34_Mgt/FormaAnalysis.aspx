<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="FormaAnalysis.aspx.cs" Inherits="SPEWEB.F_34_Mgt.FormaAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                     
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrm" runat="server" CssClass="label">General Code</asp:Label>

                                <asp:DropDownList ID="DdlForma" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnOk" runat="server" CssClass="btn btn-primary btn-sm small"
                                    OnClick="LbtnOk_Click">Ok</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height:300px;">

                    <div class="row">
                        <asp:GridView ID="gvanalysis" runat="server" AutoGenerateColumns="False" PageSize="15" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom"
                             CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details">                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDetails" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fdescription")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S-1">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s1")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S-2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS2" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s2")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S-3">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS3" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s3")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="S-4">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS4" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s4")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS5" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s5")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS6" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s6")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-7">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS7" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s7")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-8">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS8" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s8")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-9">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS9" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s9")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-10">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS10" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s10")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-11">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS11" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s11")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-12">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS12" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s12")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="S-13">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS13" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s13")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S-14">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS14" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s14")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S-15">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtgvS15" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s15")) %>'
                                            Width="60px"></asp:TextBox>
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

