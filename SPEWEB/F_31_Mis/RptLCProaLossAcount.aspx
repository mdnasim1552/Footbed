<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptLCProaLossAcount.aspx.cs" Inherits="SPEWEB.F_31_Mis.RptLCProaLossAcount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style29
        {
            width: 81px;
        }
        .style30
        {
            width: 115px;
        }
        .style31
        {
            width: 11px;
        }
        .style32
        {
            width: 539px;
        }
        .style33
        {
            width: 79px;
        }
        .style34
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 991px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="lblHeadtitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Income Statement (On Production Basis)" Width="523px" Style="border-bottom: 1px solid white;
                    border-top: 1px solid white;" Height="16px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                <asp:Label ID="lblRptType" runat="server" Visible="False" Width="99px"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Width="70px" OnClick="lnkPrint_Click"
                    Font-Underline="False" CssClass="button" BackColor="#000066" BorderColor="White"
                    BorderStyle="Solid" BorderWidth="1px">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panel11" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px"
                Width="90%">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style34">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Style="text-align: left" Text="From:" Width="80px"></asp:Label>
                        </td>
                        <td class="style29">
                            <asp:TextBox ID="txtfrmDate" runat="server" AutoCompleteType="Disabled" BorderStyle="Solid"
                                BorderWidth="1px" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="style31">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Style="text-align: left" Text="To:" Width="60px"></asp:Label>
                        </td>
                        <td class="style33">
                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" BorderStyle="Solid"
                                BorderWidth="1px" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="style32">
                            <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="16px" ForeColor="White"
                                Height="20px" OnClick="lbtnOk_Click" Style="text-align: center;" 
                                Width="52px">Ok</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvinstment" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvinstment_RowDataBound"
                            ShowFooter="True" Style="text-align: left" Width="387px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="150px">
                                                                    
                                                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Amount (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfcamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "famount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount (TK)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                              
                               
                                <asp:TemplateField HeaderText=" %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpercntage" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcntage")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcntage"))==0?"":"%" ) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
