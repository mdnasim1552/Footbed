<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymntold.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.AccOnlinePaymntold" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style76
        {
            width: 121px;
        }
        .style75
        {
            width: 125px;
        }
        .style59
        {
            height: 9px;
        }
        .style61
        {
            height: 9px;
            width: 121px;
        }
        .style73
        {
            height: 9px;
            width: 125px;
        }
        .style62
        {
            height: 28px;
        }
        .style64
        {
            height: 28px;
            width: 121px;
        }
        .style74
        {
            height: 28px;
            width: 125px;
        }
        .style66
        {
            height: 11px;
        }
        .style83
        {
        }
        .style84
        {
            height: 28px;
        }
        .style85
        {
            width: 13px;
        }
        .style86
        {
            width: 91px;
        }
        .style88
        {
            height: 9px;
            width: 2165px;
        }
        .style89
        {
            height: 28px;
            width: 2165px;
        }
        .style92
        {
            width: 134px;
        }
        .style93
        {
            height: 9px;
            width: 134px;
        }
        .style98
        {
            width: 2041px;
        }
        .style99
        {
            width: 100px;
        }
        .style100
        {
            height: 9px;
            width: 100px;
        }
        .style101
        {
            width: 2165px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>
    <table style="width: 99%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Bill Status Information" Width="636px" Style="border-bottom: 1px solid white;
                    border-top: 1px solid white;"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="PnlBill" runat="server" Width="1049px">
                            <table style="width: 100%; background: #cccc99">
                              
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" CssClass="lbltextColor" Text="Receive Date:"
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" OnTextChanged="txtReceiveDate_TextChanged"
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd.MM.yyyy" PopupButtonID="imgrecdate" TargetControlID="txtReceiveDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Image ID="imgrecdate" runat="server" Height="16" ImageUrl="~/Image/calender.png"
                                            Width="16" />
                                    </td>
                                    <td class="style83">
                                        <asp:Label ID="Label22" runat="server" CssClass="lbltextColor" Text="Bill Ref. No:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style85">
                                        <asp:TextBox ID="txtRefno" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="ddl" TabIndex="2" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" CssClass="lbltextColor" Text="Total Amount:"
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtBillAmount" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="3" Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td class="style86">
                                        <asp:Label ID="Label40" runat="server" CssClass="lbltextColor" Text="Value Date:"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style99">
                                        <asp:Image ID="imgrValdate" runat="server" Height="16px" ImageUrl="~/Image/calender.png"
                                            Width="16px" />
                                    </td>
                                    <td class="style92">
                                        <asp:TextBox ID="txtValDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" OnTextChanged="txtValDate_TextChanged"
                                            TabIndex="1" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtValDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd.MM.yyyy" PopupButtonID="imgrValdate" 
                                            TargetControlID="txtValDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style101">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" BackColor="White" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" OnClick="lbtnRefresh_Click"
                                            Style="font-size: small; height: 17px;" TabIndex="20" Width="60px">Refresh</asp:LinkButton>
                                    </td>
                                    <td class="style75">
                                    </td>
                                    <td class="style75">
                                    </td>
                                    <td class="style75">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style59">
                                        <asp:Label ID="Label7" runat="server" CssClass="lbltextColor" Text="Head of Accounts:"
                                            Width="115px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:TextBox ID="txtsrchProject" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnFindProject_Click" TabIndex="6" Style="width: 18px" />
                                    </td>
                                    <td class="style59" colspan="2">
                                        <asp:DropDownList ID="ddlProject" runat="server" Font-Bold="True" Font-Size="12px"
                                            TabIndex="7" Width="200px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlProject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label41" runat="server" CssClass="lbltextColor" Text="Adv. Amount:"
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:TextBox ID="txtAdvAmt" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="3" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        &nbsp;
                                    </td>
                                    <td class="style61">
                                        <asp:Label ID="Label37" runat="server" CssClass="lbltextColor" Text="Pay Date:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style100">
                                        <asp:Image ID="imgpaydate" runat="server" Height="16" ImageUrl="~/Image/calender.png"
                                            Width="16" />
                                    </td>
                                    <td class="style93">
                                        <asp:TextBox ID="txtpaymentdate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" OnTextChanged="txtpaymentdate_TextChanged"
                                            TabIndex="4" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtpaymentdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd.MM.yyyy" PopupButtonID="imgpaydate" TargetControlID="txtpaymentdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style88">
                                        &nbsp;<asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td class="style73">
                                        &nbsp;
                                    </td>
                                    <td class="style73">
                                        &nbsp;
                                    </td>
                                    <td class="style73">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style62">
                                        <asp:Label ID="lblDetHead" runat="server" CssClass="lbltextColor" Text="Details Head:"
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtsrchRes" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="8" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style62">
                                        <asp:ImageButton ID="ibtnRes" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnRes_Click" TabIndex="9" />
                                    </td>
                                    <td class="style84" colspan="2">
                                        <asp:DropDownList ID="ddlRescode" runat="server" Font-Bold="True" Font-Size="12px"
                                            TabIndex="10" Width="200px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="Label35" runat="server" CssClass="lbltextColor" Text="Pay To:" 
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style62">
                                        <asp:ImageButton ID="ibtnFindParty" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnFindParty_Click" TabIndex="16" />
                                    </td>
                                    <td class="style64" colspan="3">
                                        <asp:DropDownList ID="ddlPartyName" runat="server" Font-Bold="True" Font-Size="12px"
                                            TabIndex="17" Width="210px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style89">
                                        &nbsp;
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td class="style74">
                                        &nbsp;
                                    </td>
                                    <td class="style74">
                                        &nbsp;
                                    </td>
                                    <td class="style74">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        <asp:Label ID="Label38" runat="server" CssClass="lbltextColor" Text="Bill No:" Width="110px"></asp:Label>
                                    </td>
                                    <td class="style66">
                                        <asp:TextBox ID="txtsrchBillno" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="11" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style43">
                                        <asp:ImageButton ID="ibtnBillNo" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnBillNo_Click" TabIndex="12" />
                                    </td>
                                    <td class="style83" colspan="2">
                                        <asp:DropDownList ID="ddlBillList" runat="server" Font-Bold="True" Font-Size="12px"
                                            TabIndex="13" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBillList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style43">
                                        <asp:Label ID="Label33" runat="server" CssClass="lbltextColor" Text="Bill Nature:"
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txtsrchBill" runat="server" AutoCompleteType="Disabled" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" TabIndex="18" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style43">
                                        <asp:ImageButton ID="ibtnnature" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnnature_Click" TabIndex="18" />
                                    </td>
                                    <td class="style76" colspan="3">
                                        <asp:DropDownList ID="ddlBillNature" runat="server" Font-Bold="True" Font-Size="12px"
                                            TabIndex="19" Width="210px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style101">
                                        <asp:LinkButton ID="lbtnAddTable" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" ForeColor="White" OnClick="lbtnAddTable_Click"
                                            Style="font-size: small; height: 17px;" TabIndex="14" Width="60px">Add Table</asp:LinkButton>
                                    </td>
                                    <td class="style75">
                                    </td>
                                    <td class="style75">
                                    </td>
                                    <td class="style75">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Style="margin-top: 0px" Width="831px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Record No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Head">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adv. Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFAdvTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAdvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFNetTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNetamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvValdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Appx. payment Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Nature" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
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

