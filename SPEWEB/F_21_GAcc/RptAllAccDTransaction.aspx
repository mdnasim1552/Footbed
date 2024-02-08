<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAllAccDTransaction.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAllAccDTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style33
        {
            width: 51px;
        }
        .style34
        {
            width: 43px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 11px;
            font-weight: normal;
            margin-right: 0px;
        }
        .style32
        {
            width: 12px;
        }
        .style35
        {
            width: 848px;
        }
        .style36
        {
            width: 215px;
        }
        .style38
        {
            width: 106px;
        }
        .style39
        {
            width: 36px;
        }
        .style40
        {
            color: #FFFFFF;
            }
        .style58
        {
            width: 75px;
        }
        .style60
        {
            width: 17px;
        }
        .style61
        {
            width: 48px;
        }
        .style62
        {
            width: 38px;
        }
        .style64
        {
        }
        .style65
        {
            width: 79px;
        }
        .style66
        {
            width: 42px;
        }
        .style67
        {
            width: 55px;
        }
        .style68
        {
            width: 224px;
        }
        .style71
        {
            width: 6px;
        }
        .style77
        {
            width: 7px;
        }
        .style78
        {
            width: 15px;
        }
        .style79
        {
            width: 37px;
        }
        .style80
        {
            width: 41px;
        }
        .style81
        {
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="LblTitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="MONEY RECEIPT  INFORMATION VIEW/EDIT" Width="500px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="8">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                    BorderWidth="1px">
                            <table style="width: 100%;">
                              
                                <tr>
                                    <td class="style62">
                                        <asp:Label ID="Label22" runat="server" CssClass="style40" Font-Bold="True" 
                                            Style="text-align: left" Text="From:" Width="50px" Height="16px"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtfromdate" runat="server" 
                                            Font-Bold="True" Width="85px" BorderStyle="None" Height="16px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                       
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style60">
                                        <asp:Label ID="Label23" runat="server" CssClass="style40" Font-Bold="True" 
                                            Style="text-align: right" Text="To:" Height="16px" Width="40px"></asp:Label>
                                    </td>
                                    <td class="style61">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Width="87px" Height="16px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                     
                                    </td>
                                    <td class="style36">
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" OnClick="lbtnShow_Click" 
                                            Width="40px" BorderWidth="1px" ForeColor="White" 
                                            style="text-align: center">Show</asp:LinkButton>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td align="left" class="style33">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td class="style32">
                                        &nbsp;</td>
                                    <td class="style39">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                       
                                <table style="width: 100%;">
                                   
                                    <tr>
                                        <td class="style64" colspan="12">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td class="style65">
                                                            <asp:Label ID="lblVoucherCash" runat="server" Font-Size="12px" ForeColor="White" 
                                                                style="font-weight: 700; text-align: left" Text="Voucher Type :" 
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="style66">
                                                            <asp:DropDownList ID="ddlVoucharCash" runat="server" BackColor="#CCFFCC" 
                                                                Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" Width="140px">
                                                                <asp:ListItem Value="C">Cash Voucher</asp:ListItem>
                                                                <asp:ListItem Value="B">Bank Voucher</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="ALL Voucher">ALL Voucher</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style67">
                                                            <asp:Label ID="lblGroup" runat="server" Font-Size="12px" ForeColor="White" 
                                                                style="font-weight: 700; text-align: right" Text="Group:" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style68" colspan="6">
                                                            <asp:RadioButtonList ID="rbtnGroup" runat="server" BackColor="#BBBB99" 
                                                                BorderColor="#FFCC00" BorderStyle="None" Font-Bold="True" Font-Size="14px" 
                                                                RepeatColumns="6" RepeatDirection="Horizontal" Style="text-align: center" 
                                                                Width="235px">
                                                              
                                                                <asp:ListItem>Receipt</asp:ListItem>
                                                                <asp:ListItem>Payment</asp:ListItem>
                                                                <asp:ListItem Selected="True">Both</asp:ListItem>
                                                                
                                                            </asp:RadioButtonList>
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
                                                    <tr>
                                                        <td class="style65">
                                                            <asp:Label ID="lblAmount0" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: left;" Text="Amount:" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="style66">
                                                            <asp:DropDownList ID="ddlSrchCash" runat="server" AutoPostBack="True" 
                                                                Font-Bold="True" Font-Size="12px" 
                                                                onselectedindexchanged="ddlSrchCash_SelectedIndexChanged" Width="140px">
                                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                                <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                                <asp:ListItem Value="between">Between</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style67">
                                                            <asp:TextBox ID="txtAmountC1" runat="server" BorderStyle="None" 
                                                                Font-Bold="True" Height="16px" style="text-align: right" Width="85px"></asp:TextBox>
                                                        </td>
                                                        <td class="style71">
                                                            <asp:Label ID="lblToCash" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right;" Text="To:" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style79">
                                                            <asp:TextBox ID="txtAmountC2" runat="server" BorderStyle="None" 
                                                                Font-Bold="True" Height="16px" style="text-align: right" Visible="False" 
                                                                Width="80px"></asp:TextBox>
                                                        </td>
                                                        <td class="style80">
                                                            <asp:ImageButton ID="imgbtnSearchVoucherCash" runat="server" Height="16px" 
                                                                ImageUrl="~/Image/find_images.jpg" onclick="imgbtnSearchVoucherCash_Click" 
                                                                Width="16px" />
                                                        </td>
                                                        <td class="style81">
                                                            &nbsp;</td>
                                                        <td class="style77">
                                                            &nbsp;</td>
                                                        <td class="style78">
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
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="style64">
                                            <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Receipts" Width="162px" Visible="False"></asp:Label>
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
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Label ID="lblPaymentCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Payment" Width="669px" Visible="False" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvcashbookp" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                       <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px" 
                                                Visible="False" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="973px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            
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
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

