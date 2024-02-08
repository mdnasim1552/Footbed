﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RecHRCodeBook.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_81_Rec.RecHRCodeBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 70px;">
                  
                        <div class="row">
                            <div class="col-md-2 pading5px">
                                <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName160" Text="Select Code Book:"></asp:Label>
                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                            <div class="col-md-2 pading5px">
                                <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-sm btn-primary "></asp:LinkButton>

                                <asp:LinkButton ID="lnkcancel" runat="server" Text="Change" Visible="False" OnClick="lnkcancel_Click" CssClass="btn btn-sm btn-primary"></asp:LinkButton>

                            </div>
                            <div class="col-md-2 pading5px">
                                <div class="msgHandSt">
                                    <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                        OnRowUpdating="grvacc_RowUpdating" Width="726px" ShowFooter="True" PageSize="15">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                            Visible="False" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True">
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod2"))+"-" %>'
                                        Width="20px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="3"
                                        Style="border-style: none; border-color: midnightblue; font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                        Width="50px" Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="14px"
                                        OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                        Width="150px">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvUnit" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtype" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="%">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvtxtpercnt" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px" Style="text-align: right"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvpercnt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <AlternatingRowStyle BackColor="" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

