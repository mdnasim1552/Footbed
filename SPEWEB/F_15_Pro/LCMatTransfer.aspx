<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LCMatTransfer.aspx.cs" Inherits="SPEWEB.F_15_Pro.LCMatTransfer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style12
        {
            width: 35px;
        }
        .style13
        {
            width: 51px;
        }
        .style14
        {
            width: 240px;
        }
        .style15
        {
            width: 9px;
        }
        .style16
        {
            color: #FFFFFF;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 911px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                                
                    
                    
                    
                    
                
                    Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1; margin-top: 0px;" Text="Material Transfer Information Input/Edit Screen"
                                Width="400px" BorderStyle="Inset" BackColor="Transparent" 
                    BorderWidth="2px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
            </td>
            <td class="style59">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="True"
                                Font-Size="18px" 
                    Style="background-color: #fffbf1; text-align: center" Width="69px" 
                    BorderStyle="Inset" BorderColor="#FFC080" 
                    BorderWidth="2px" onclick="lnkPrint_Click" >Print</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td class="style82">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="TEXT-ALIGN: right" Text="Last Trans Date:" Width="100px" 
                            CssClass="style16"></asp:Label>
                    </td>
                    <td class="style81">
                        <asp:Label ID="lblLastTransDate" runat="server" Font-Bold="True" 
                            Font-Size="12px" Height="16px" 
                            
                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                            Width="80px"></asp:Label>
                    </td>
                    <td class="style42">
                        <asp:LinkButton ID="lbtnPrevTransList" runat="server" Font-Bold="True" 
                            Font-Size="12px"  
                            style="text-align: right; height: 15px; color: #FFFFFF;" Width="90px" 
                            onclick="lbtnPrevTransList_Click">Prev. Trans List:</asp:LinkButton>
                    </td>
                    <td class="style80">
                        <asp:DropDownList ID="ddlPrevISSList" runat="server" Font-Size="12px" 
                            Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td class="style78" colspan="2">
                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px" 
                            Height="23px" onclick="lbtnOk_Click" 
                            style="text-align: center; background-color: #99FFCC" Width="52px">Ok</asp:LinkButton>
                    </td>                    
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style85">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style82">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="TEXT-ALIGN: right" Text="LastTrans No.:" Width="100px" 
                            CssClass="style16"></asp:Label>
                    </td>
                    <td class="style81">
                        <asp:Label ID="lblLastTransNo" runat="server" Font-Bold="True" Font-Size="12px" 
                            
                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                            Width="80px"></asp:Label>
                    </td>
                    <td class="style42">
                        <asp:Label ID="lblProjectFromList" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="TEXT-ALIGN: left" Text="From Order List:" Width="96px" 
                            Height="16px" CssClass="style16"></asp:Label>
                    </td>
                    <td class="style80">
                        <asp:DropDownList ID="ddlprjlistfrom" runat="server" 
                            Width="300px" AutoPostBack="True" 
                            onselectedindexchanged="ddlprjlistfrom_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblddlProjectFrom" runat="server" __designer:wfdid="w4" 
                            BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="295px"></asp:Label>
                    </td>
                    <td class="style84">
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                            Height="16px" style="TEXT-ALIGN: right" Text="Curr.Trans Date:" 
                            Width="97px" CssClass="style16"></asp:Label>
                    </td>
                    <td class="style83">
                        <asp:TextBox ID="txtCurTransDate" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                            Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" 
                            Format="dd.MM.yyyy" TargetControlID="txtCurTransDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style85" >
                        &nbsp;</td>
                    
                    
                </tr>
                <tr>
                    <td class="style82">
                        &nbsp;</td>
                    <td class="style81">
                        &nbsp;</td>
                    <td class="style42">
                        &nbsp;</td>
                    <td class="style80">
                        &nbsp;</td>
                    <td class="style84">
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="TEXT-ALIGN: right" Text="Curr. Trans No.:" Width="100px" 
                            CssClass="style16"></asp:Label>
                    </td>
                    <td class="style83">
                        <asp:Label ID="lblCurTransNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                            
                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                            Width="40px"></asp:Label>
                        <asp:TextBox ID="txtCurTransNo2" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                            Width="45px">00001</asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style85">
                        &nbsp;</td>
 
                </tr>
                <tr>
                    <td class="style82">
                        &nbsp;</td>
                    <td class="style81">
                        &nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="text-align: right" Text="To Order List:" Width="90px" 
                            CssClass="style16"></asp:Label>
                    </td>
                    <td class="style80">
                        <asp:DropDownList ID="ddlprjlistto" runat="server" 
                            style="margin-left: 0px" Width="300px">
                        </asp:DropDownList>
                        <asp:Label ID="lblddlProjectTo" runat="server" __designer:wfdid="w4" 
                            BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="295px"></asp:Label>
                    </td>
                    <td class="style84">
                        &nbsp;</td>
                    <td class="style83">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style85">
                        &nbsp;</td>
                    
                     
                </tr>
                <tr>
                    <td class="style82">
                        &nbsp;</td>
                    <td class="style81">
                        &nbsp;</td>
                    <td class="style42">
                        &nbsp;</td>
                    <td class="style80">
                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="12px" 
                            ForeColor="White" BackColor="Red"></asp:Label>
                    </td>
                    <td class="style78" colspan="2">
                        &nbsp;</td>
                    <td class="style34" colspan="3">
                        &nbsp;</td>
                     
                </tr>
            </table>
            <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                <table style="width: 100%; background-color: #C1D2C4;">
                    <tr>
                        <td colspan="14" style="text-align: center">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style13">
                                            <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" 
                                                style="TEXT-ALIGN: right" Text="Resource List:" Width="90px"></asp:Label>
                                        </td>
                                        <td class="style14" style="text-align: left">
                                            <asp:DropDownList ID="ddlreslist" runat="server" AutoPostBack="false" 
                                               Width="300px">
                                            </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ListSearchExt2" runat="server" 
                                                QueryPattern="Contains" TargetControlID="ddlreslist">
                                            </cc1:ListSearchExtender>
                                        </td>
                                        <td class="style15">
                                            <asp:LinkButton ID="lnkselect" runat="server" Font-Bold="True" 
                                                onclick="lnkselect_Click" style="text-align: left">Select</asp:LinkButton>
                                        </td>
                                        <td align="left">
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
                        <td colspan="14" 
                            style="height: 200px; vertical-align: top; text-align: left;" align="left">
                          
                                    <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" 
                                                            ShowFooter="True" Width="691px" 
                                        style="margin-right: 0px">
                                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrescode" runat="server" 
                                                
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' 
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunit" runat="server" 
                                                style="FONT-SIZE: 12px; text-align: center;" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>' 
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid" 
                                                style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" class="UpdateButton"
                                                Font-Size="16px" onclick="lnkupdate_Click"  Width="80px">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px"  HorizontalAlign="right"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid" 
                                                style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>                                        
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px"  HorizontalAlign="right"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAmt" runat="server" 
                                                style="FONT-SIZE: 12px; text-align: right;" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right"  />
                                        <FooterStyle HorizontalAlign="Right" />
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
                        <td class="style52">
                            &nbsp;</td>
                        <td class="style43">
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
                        <td class="style69" colspan="3">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td class="style60">
                            &nbsp;</td>
                        <td class="style53">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style52">
                            &nbsp;</td>
                        <td class="style43">
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
                        <td class="style69" colspan="3">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td class="style60">
                            &nbsp;</td>
                        <td class="style53">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style52">
                            &nbsp;</td>
                        <td class="style43">
                            &nbsp;</td>
                        <td class="style12">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td colspan="4">
                            &nbsp;</td>
                        <td colspan="3">
                            &nbsp;</td>
                        <td class="style70">
                            &nbsp;</td>
                        <td class="style66">
                            &nbsp;</td>
                        <td class="style66">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style52">
                            &nbsp;</td>
                        <td class="style43">
                            &nbsp;</td>
                        <td class="style12">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="style69" colspan="3">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td class="style60">
                            &nbsp;</td>
                        <td class="style53">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



