<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="MatTransfer02.aspx.cs" Inherits="SPEWEB.F_13_CWare.MatTransfer02" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 83px;
        }
        .style16
        {
            color: #FFFFFF;
        }
        .style17
        {
            width: 65px;
        }
        .style21
        {
        }
        .style23
        {
            width: 29px;
        }
        .style24
        {
            width: 91px;
        }
        .style25
        {
            width: 1427px;
        }
        .style26
        {
            width: 81px;
        }
        .style27
        {
            width: 75px;
        }
        .style28
        {
            width: 13px;
        }
        .style29
        {
            width: 316px;
        }
        .style30
        {
            width: 716px;
        }
        .style31
        {
            width: 226px;
        }
        .style32
        {
            width: 250px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 915px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                                
                    
                    
                    
                    
                Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1" Text="Materials Transfer Information Input/Edit Screen"
                                Width="400px" BorderStyle="Inset" BackColor="Transparent" 
                    BorderWidth="2px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="True"
                                Font-Size="18px" 
                    Style="background-color: #fffbf1; text-align: center" Width="69px" 
                    BorderStyle="Inset" BorderColor="#FFC080" 
                    BorderWidth="2px" onclick="lnkPrint_Click" >Print</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="pnlMain" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style17">
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Trans Date.:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtCurTransDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="Label9" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Trans No.:" Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCurTransNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                            Width="45px">00001</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Ref. No:" Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtrefno" runat="server" BorderStyle="None" Width="105px"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="text-align: center" Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style30">
                                        &nbsp;</td>
                                    <td class="style31">
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
                                    <td class="style17">
                                        <asp:Label ID="lblPreViousList" runat="server" CssClass="style16" 
                                            Font-Bold="True" Font-Size="12px" style="TEXT-ALIGN: left" Text="Previous:" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtSrchPrevious" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        <asp:ImageButton ID="ImgbtnFindPrevious" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPrevious_Click" 
                                            TabIndex="6" Width="21px" />
                                    </td>
                                    <td class="style21" colspan="8">
                                        <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" Style="border-right: midnightblue 1px solid;
                                            border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid;
                                            background-color: #fffbf1" TabIndex="7" Width="355px">
                                        </asp:DropDownList>
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
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="PanelSub" runat="server" BorderColor="Maroon" 
                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="Label10" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Req  No.:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtReqSearch" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style28">
                                        <asp:ImageButton ID="imgbtnReq" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnReq_Click" TabIndex="6" 
                                            Width="21px" />
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlReqList" runat="server" Font-Size="12px" 
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1" 
                                            TabIndex="7" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnSelect_Click" 
                                            style="text-align: center" Width="52px">Select</asp:LinkButton>
                                    </td>
                                    <td class="style32">
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
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
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" 
                            onrowdeleting="grvacc_RowDeleting" ShowFooter="True" Width="501px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />



                                <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatCode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfrmDesc" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfdesc")) %>' 
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>




                                
                                <asp:TemplateField HeaderText="To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtodesc" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttdesc")) %>' 
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resource Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                 
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvspcfdesc" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" 
                                            style="FONT-SIZE: 11px; text-align: center;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lnktotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvqty" runat="server" BackColor="Transparent" BorderStyle="None" 
                                            style="text-align: right; font-size: 11px;" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lnkupdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" 
                                            style="text-align: right; font-size: 11px;" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmount" runat="server" style="text-align: right" 
                                            Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server" 
                                            style="FONT-SIZE: 11px; text-align: right;" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="white" 
                                        HorizontalAlign="right" VerticalAlign="Middle" />
                                   
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
                    <td class="style8">
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

