<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccVouher02.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccVouher02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            width: 54px;
        }
        .style8
        {
        }
        .style9
        {
            width: 42px;
        }
        .style10
        {
            width: 517px;
        }
        .style11
        {
            width: 83px;
        }
        .style12
        {
            width: 95px;
        }
        .style13
        {
        }
        .style14
        {
            width: 58px;
        }
        .style15
        {
            width: 35px;
        }
        .style16
        {
            width: 45px;
        }
        .style17
        {
            width: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table  style="width: 99%;">
    <tr>
        <td class="style22">
                    <asp:Label ID="LblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                        ForeColor="Yellow" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;" Text="VOUCHER INFORMATION VIEW/EDIT" 
                        Width="500px"></asp:Label>
                </td>
        <td>
                                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                            </td>
        <td align="right">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
        <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click">PRINT</asp:LinkButton>
                </td>
    </tr>
    </table>
    
            
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Pnlmain" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style7">
                                        <asp:Label ID="lblVoucherNo" runat="server" CssClass="label2" 
                                            Text="Voucher No. :" Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="lblvalVoucherNo" runat="server" BackColor="White" Width="90px" 
                                            Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <asp:Label ID="lblVouDate" runat="server" CssClass="label2" 
                                            Text="Voucher Date :" Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblvalVoucherDate" runat="server" BackColor="White" Width="80px" 
                                            Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style10">
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
                                    <td class="style7">
                                        <asp:Label ID="lblBankDescription" runat="server" CssClass="label2" 
                                            Text="Bank Name :" Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style8" colspan="3">
                                        <asp:Label ID="lblValBankDescription" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                    </td>
                                    <td class="style10">
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
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="337px">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Head of Accounts">
                                   
                                    <FooterTemplate>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc2" runat="server" 
                                            Font-Size="11px" 
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "          " + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "") %>' 
                                            Width="250px" TabIndex="75"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                               
                              
                                <asp:TemplateField HeaderText="Issue No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblisuno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                            TabIndex="76" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                              
                                <asp:TemplateField HeaderText="Cheque No">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvChequeno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                            TabIndex="76" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvChequeDate" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px" 
                                            ForeColor="Black" TabIndex="77" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>' 
                                            Width="80px"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextbox" Font-Size="12px" ForeColor="Black" TabIndex="78" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="79" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            
                                <asp:TemplateField HeaderText="Payto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPayto" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="80" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                
                            
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instade Of Issue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinsissueno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Maroon" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style11">
                                        &nbsp;</td>
                                    <td class="style17">
                                        &nbsp;</td>
                                    <td class="style14">
                                        &nbsp;</td>
                                    <td class="style15">
                                        &nbsp;</td>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style12">
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
                                    <td class="style11">
                                        <asp:Label ID="lblNarration" runat="server" CssClass="label2" 
                                            Text="Narration :" Width="120px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style13" colspan="5">
                                        <asp:Label ID="lblvalNarration" runat="server" BackColor="White" Height="40px" 
                                            Width="590px" Font-Size="12px"></asp:Label>
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
                                    <td class="style11">
                                        &nbsp;</td>
                                    <td class="style17">
                                        &nbsp;</td>
                                    <td class="style14">
                                        &nbsp;</td>
                                    <td class="style15">
                                        &nbsp;</td>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style12">
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

