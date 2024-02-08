<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="MSLCStatus.aspx.cs" Inherits="SPEWEB.F_34_Mgt.MSLCStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style11
        {
            width: 26px;
        }
        .style9
        {
            width: 7px;
        }
        .style8
        {
            width: 195px;
        }
        .style12
        {
            height: 20px;
            width: 415px;
        }
        .style13
        {
            height: 20px;
            width: 66px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="LC STATUS INFORMATION" Width="500px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td class="style50">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style56">
                                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                                        Style="font-size: 11px" Width="130px">
                                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" 
                    style="color: #FFFFFF; width: 40px; height: 17px;" BackColor="#003366" 
                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px">PRINT</asp:LinkButton>
            </td>
        </tr>
        </table>
        
                

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style11">
                                        <asp:Label ID="lblmasterlc" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Text="Master L/C:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style13">
                                        <asp:TextBox ID="txtemlcsrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderStyle="None" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style9">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnOk_Click" style="color: #FFFFFF; height: 17px;">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="lblmsg" runat="server" __designer:wfdid="w5" BackColor="Red" 
                                            Font-Bold="True" ForeColor="White" 
                                            Style="font-weight: bold; font-size: 14px; text-align: left"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvLcStatus" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" onpageindexchanging="gvLcStatus_PageIndexChanging" 
                            PageSize="15" ShowFooter="True" Width="335px" 
                            onrowdeleting="gvLcStatus_RowDeleting">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="mclcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmlccod" runat="server" Font-Size="12px" 
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                 
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDescription" runat="server" Font-Size="12px" 
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpLCStatus" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnUpLCStatus_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chklcstatus" runat="server" 
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcstatus"))=="True" %>' />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

