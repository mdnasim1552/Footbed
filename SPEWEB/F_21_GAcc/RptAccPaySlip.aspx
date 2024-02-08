<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccPaySlip.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccPaySlip" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style5
        {
            width: 44px;
        }
        .style6
        {
            width: 48px;
        }
        .style7
        {
            width: 52px;
        }
        .style8
        {
            width: 11px;
        }
        .style9
        {
            width: 285px;
        }
        .style10
        {
            width: 49px;
        }
        .style11
        {
            width: 73px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="PAY SLIP INFORMATION VIEW/EDIT" Width="500px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" ></asp:Label>
            </td>
            <td>
                                                    <asp:Label ID="lbljavascript" 
                    runat="server"></asp:Label>
            </td>
            <td>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
        </table>
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td class="style6">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Height="16px" Text="Get Acc. Heads:" Width="85px"></asp:Label>
                                    </td>
                                    <td class="style7">
                                        <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" 
                                            CssClass="ddl" Height="17px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:ImageButton ID="IbtnSearchAcc" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="IbtnSearchAcc_Click" Width="16px" />
                                    </td>
                                    <td class="style9" colspan="2">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="ddl" 
                                            Height="19px" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="button" 
                                            Font-Bold="True" onclick="lnkShowLedger_Click" Width="55px">Show </asp:LinkButton>
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
                                </tr>
                                <tr>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td class="style6">
                                        <asp:Label ID="lblDate" runat="server" CssClass="label2" Text=" Date:" 
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style7">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="ddl" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="lblDateto" runat="server" CssClass="label2" Text="To:" 
                                            Width="16px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="ddl" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style11">
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
                       
                            <asp:GridView ID="gvpayslip" runat="server" AutoGenerateColumns="False" 
                                                            ShowFooter="True" Width="786px" 
                                onrowdatabound="gvpayslip_RowDataBound">
                                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebelL" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' 
                                            widht="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" __designer:wfdid="w1" 
                                            CssClass="GridLebelL" Font-Underline="False" Target="_blank" 
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                           
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription" runat="server" CssClass="GridLebelL" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>' 
                                            Width="400px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Advanced">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' 
                                            width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjustment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' 
                                            width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                          
                                
                            </Columns>
                          <FooterStyle BackColor="#333333" />
                                                            <PagerStyle HorizontalAlign="Center" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

