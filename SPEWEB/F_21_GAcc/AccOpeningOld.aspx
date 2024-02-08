<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccOpeningOld.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccOpeningOld" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style18
        {
            width: 472px;
        }
        .style19
        {
            width: 186px;
        }
        .style25
        {
            width: 603px;
        }
        .style26
        {
            width: 195px;
        }
        .style27
        {
            width: 91px;
        }
        .style28
        {
            width: 100px;
        }
        .style29
        {
            height: 10px;
            width: 217px;
        }
        .style30
        {
            width: 83px;
        }
        .style31
        {
            width: 20px;
        }
        .style32
        {
            width: 754px;
        }
        .style33
        {
            width: 82px;
        }
        .style34
        {
            width: 90px;
        }
        </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:907px;" >
        <tr>
            <td class="style25">
                    <asp:Label ID="lblGeneralAcc" runat="server" Text="ACCOUNTS OPENING INFORMATION INPUT/EDIT SCREEN" 
                        CssClass="label" Width="572px"></asp:Label>
                </td>
            <td class="style28">
                    &nbsp;</td>
            <td class="style26">
                    &nbsp;</td>
            <td class="style27">
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onmouseover="style.color='red'" onmouseout="style.color='blue'"
                         Width="72px" onclick="lnkPrint_Click">PRINT</asp:LinkButton>
                </td>
        </tr>
        </table>
                
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                           
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style33">
                                                    <asp:Label ID="Label2" runat="server" CssClass="label2" Text="Opening Date" 
                                                        Width="80px"></asp:Label>
                                                </td>
                                                <td class="style34">
                                                    <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled" 
                                                        BorderStyle="None" Width="105px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" 
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" BackColor="Red"></asp:Label>
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
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style33">
                                                    <asp:Label ID="lblacccode1" runat="server" CssClass="label2" 
                                                        Text="Accounts Code" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style34">
                                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" 
                                                        BorderStyle="None" Width="105px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/search-button-on.gif" onclick="ImageButton1_Click" 
                                                        Width="50px" />
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
                                <td class="style5" colspan="11">
                                    <asp:GridView ID="dgv2" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" BackColor="#99CCCC" BorderColor="#7FBF41" 
                                        BorderStyle="Solid" BorderWidth="2px" onrowcreated="dgv2_RowCreated" 
                                        PagerSettings-Position="Bottom" PagerStyle-BackColor="#4A89BC" 
                                        PagerSettings-Visible="false"
                                        PagerStyle-HorizontalAlign="Center" RowStyle-Font-Size="12px" ShowFooter="True" 
                                        Width="910px" onrowcommand="dgv2_RowCommand" PageSize="12">
                                        <PagerSettings Visible="False" />
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ActCode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head of Accounts">
                                                <FooterTemplate>
                                                    <table style="width: 50%;">
                                                        <tr>
                                                            <td class="style20">
                                                                <asp:DropDownList ID="dgv2ddlPageNo" runat="server" AutoPostBack="True" 
                                                                    Font-Bold="True" Font-Size="14px" 
                                                                    onselectedindexchanged="dgv2ddlPageNo_SelectedIndexChanged" 
                                                                    style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                                                    Width="180px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="style21">
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td class="style22">
                                                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="UpdateButton" 
                                                                    onclick="lnkFinalUpdate_Click" onmouseout="style.color='White'" 
                                                                    onmouseover="style.color='#FF9999'" Text="Final Upate" Width="90px" 
                                                                    BackColor="Black" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                    Font-Bold="True" Font-Size="12px"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL" 
                                                        
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                        Font-Size="11px" Width="400px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level" 
                                                ItemStyle-HorizontalAlign="Center">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="LnkfTotal" runat="server" onclick="LnkfTotal_Click" 
                                                        style="color: #FFFFFF; font-weight: 700">Total :</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="gvlnkLevel" runat="server" onclick="gvlnkLevel_Click" 
                                                        onmouseover="style.color='#FF9999'" onmouseout="style.color='White'"
                                                        
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>' 
                                                        Width="50px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" 
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        CssClass="GridTextbox" Width="103px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" 
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        CssClass="GridTextbox" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="103px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" 
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        CssClass="GridTextbox" 
                                                        Width="103px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" 
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        CssClass="GridTextbox" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="103px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                        <FooterStyle BackColor="#5E7BAE" />
                                        <PagerStyle BackColor="#4A89BC" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5E7BAE" BorderStyle="Solid" BorderWidth="2px" 
                                            Font-Bold="True" Font-Size="14px" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="#6A8B92" BorderColor="#FF66CC" 
                                            BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" colspan="11">
                                    <asp:Panel ID="Panel2" runat="server" BackColor="#CECEB5">
                                        <table style="width:100%;">
                                            <tr>
                                                <td align="center" colspan="21" 
                                                    style="color: #000080; font-weight: 700; background-color: #4FA7FF" 
                                                    valign="middle">
                                                    <asp:Label ID="lblacccode2" runat="server" CssClass="label2" Font-Bold="True" 
                                                        Font-Names="Verdana" Font-Size="16px" Text="Resource Entry Screen"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="lblacccode" runat="server" Text="Accounts Code" 
                                                        CssClass="label2" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style15" colspan="15">
                                                    <asp:TextBox ID="txtActcode" runat="server" BackColor="#EFBEDF" 
                                                        BorderColor="#EFBEDF" Width="400px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px" 
                                                        ForeColor="White" style=" text-align: right;" Text="Page Size" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td class="style29" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>150</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>300</asp:ListItem>
                                                         <asp:ListItem>600</asp:ListItem>
                                                          <asp:ListItem>900</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style15">
                                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" Height="16px" 
                                                        onclick="lnkSubmit_Click" onmouseout="style.color='White'" 
                                                        onmouseover="style.color='#FF9999'" Width="77px">Home</asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <%-- <asp:GridView ID="dgv3" runat="server" AllowPaging="True" 
                                                        AutoGenerateColumns="False" BackColor="#F0F0F0" ShowFooter="True" 
                                                        Width="645px" Height="101px" onpageindexchanging="dgv3_PageIndexChanging">
                                                        <PagerSettings Position="TopAndBottom" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <RowStyle BackColor="#999966" />--%>
                                                        
                                                       <%-- <FooterStyle BackColor="#333300" Font-Size="12px" ForeColor="#FFFFCC" />
                                                        <HeaderStyle BackColor="#333300" Font-Size="12px" ForeColor="#FFFFCC" />
                                                        <AlternatingRowStyle BackColor="#EFBEDF" />
                                                    </asp:GridView>--%>
                                                    &nbsp;</td>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="Label5" runat="server" 
                                                        style="font-weight: 700; text-align: right; " Text="Resource Code" 
                                                        Width="115px" Font-Size="12px" ForeColor="White"></asp:Label>
                                                </td>
                                                <td class="style31">
                                                    <asp:TextBox ID="txtResSearch" runat="server" AutoCompleteType="Disabled" 
                                                        EnableTheming="True" BorderStyle="None" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" 
                                                        ImageUrl="~/Image/search-button-on.gif" onclick="ImageButton2_Click" 
                                                        Width="41px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style30">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style15">
                                                    &nbsp;</td>
                                                <td class="style15">
                                                    &nbsp;</td>
                                                <td class="style15">
                                                    &nbsp;</td>
                                                <td class="style32">
                                                    <asp:Label ID="lblmsg01" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White"></asp:Label>
                                                </td>
                                                <td align="right" valign="middle">
                                                    &nbsp;</td>
                                                <td align="left" class="style29" valign="middle">
                                                    &nbsp;</td>
                                                <td class="style15">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="21">
                                                   <%-- <asp:GridView ID="dgv3" runat="server" AllowPaging="True" 
                                                        AutoGenerateColumns="False" BackColor="#F0F0F0" ShowFooter="True" 
                                                        Width="645px" Height="101px" onpageindexchanging="dgv3_PageIndexChanging">
                                                        <PagerSettings Position="TopAndBottom" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <RowStyle BackColor="#999966" />--%>
                                                        
                                                         <asp:GridView ID="dgv3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" onpageindexchanging="dgv3_PageIndexChanging" 
                            ShowFooter="True" Width="831px">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                        
                                                        <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="gvlblrescode" runat="server"  
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Res.Description" 
                                                                FooterStyle-HorizontalAlign="Right">
                                                               
                                                                <ItemTemplate>
                                                                    <asp:Label ID="gvlblResDesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                        Width="400px" Font-Size="12px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Unit" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="gvlblresunit" runat="server" 
                                                                        
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                                        Width="40px" Font-Size="11px"></asp:Label>
                                                                </ItemTemplate>
                                                            
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity"  ItemStyle-HorizontalAlign="Center">
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True" 
                                                                        Font-Size="12px" onclick="lnkbtnUpdateRes_Click" BorderColor="White" 
                                                                        BorderStyle="None" BorderWidth="1px" CssClass="UpdateButton">Update</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                                        CssClass="GridTextbox" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="106px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="gvlnkFTotal" runat="server" Font-Bold="True" 
                                                                        ForeColor="White" onclick="gvlnkFTotal_Click">Total 
                                                                    :</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="gvtxtRate" runat="server" BackColor="Transparent" 
                                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                                        CssClass="GridTextbox" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="106px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dr. Amount" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent" 
                                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                                        CssClass="GridTextbox" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="106px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent" 
                                                                        Font-Bold="True" Font-Size="11px" ForeColor="Beige" 
                                                                        BorderColor="Transparent" BorderStyle="None" CssClass="GridTextbox" 
                                                                        Width="116px" ReadOnly="True"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cr. Amount" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent" 
                                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                                        CssClass="GridTextbox" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="106px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent" 
                                                                        Font-Bold="True" Font-Size="11px" ForeColor="Beige" 
                                                                        BorderColor="Transparent" BorderStyle="None" CssClass="GridTextbox" 
                                                                        Width="106px"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                          <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                             <FooterStyle BackColor="#333300" BorderStyle="None" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>

                                                       <%-- <FooterStyle BackColor="#333300" Font-Size="12px" ForeColor="#FFFFCC" />
                                                        <HeaderStyle BackColor="#333300" Font-Size="12px" ForeColor="#FFFFCC" />
                                                        <AlternatingRowStyle BackColor="#EFBEDF" />
                                                    </asp:GridView>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="style16">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td class="style7" colspan="4">
                                    &nbsp;</td>
                                <td class="style19">
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

