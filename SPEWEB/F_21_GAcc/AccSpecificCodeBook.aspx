<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccSpecificCodeBook.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccSpecificCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style16
        {
            width: 339px;
        }
        .style17
        {
            width: 916px;
        }
        .style20
        {
            width: 268435456px;
            height: 10px;
        }
        .style24
        {
            width: 575px;
            height: 10px;
        }
        .style25
        {
            width: 22px;
            height: 10px;
        }
        .style26
        {
            width: 685px;
        }
        .style27
        {
            width: 163px;
            height: 10px;
        }
        .style29
        {
            width: 84px;
            height: 10px;
        }
        .style30
        {
            width: 43px;
        }
        .style31
        {
            width: 118px;
        }
        .style32
        {
            width: 103px;
            height: 10px;
        }
        .style33
        {
            width: 20px;
        }
        .style34
        {
            width: 10px;
        }
    </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 912px">
        <tr>
            <td class="style26">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" 
                    Font-Size="25px" ForeColor="Navy" Height="18px" Text="SPECIFICATION CODE BOOK INFORMATION INPUT/VIEW SCREEN" 
                    Width="550px" style="font-size: 18px" BackColor="#9999FF"></asp:Label>
            </td>
            <td class="style47">
                <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;</td>
            <td>
                <asp:LinkButton runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Size="12px" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; font-size: 12px;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style17">
                <tr>
                    <td class="style31">
                    </td>
                    <td class="style29">
                        &nbsp;</td>
                    <td class="style15">
                        &nbsp;</td>
                    <td class="style25">
                        &nbsp;</td>
                    <td class="style24">
                        <asp:Label ID="ConfirmMessage" runat="server" Font-Italic="True" 
                            Font-Size="18px" Width="400px" style="color: #FFFF66; font-size: 16px;"></asp:Label>
                    </td>
                    <td class="style27">
                        </td>
                    <td class="style20">
                    </td>
                </tr>
 
                <tr>
                    <td class="style15" colspan="7">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style30">
                                        &nbsp;</td>
                                    <td class="style32">
                                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="16px" ForeColor="#003366" Height="16px" 
                                            style="FONT-SIZE: 18px; TEXT-ALIGN: right; color: #FFFFFF;" Text="Group:" 
                                            Width="90px"></asp:Label>
                                    </td>
                                    <td class="style25">
                                        <asp:TextBox ID="txtsrch" runat="server" 
                                            BorderStyle="None" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style34">
                                        <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" Visible="False" 
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCodeBookSegment" runat="server" BackColor="#68AED0" 
                                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="129px">
                                            <asp:ListItem Value="2">Maic Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="18px" style="margin-bottom: 1px; color: #FFFFFF;" Visible="False" 
                                            Width="119px">(Details Code)</asp:Label>
                                        <asp:LinkButton ID="lnkok" runat="server" Font-Bold="True" Font-Size="16px" 
                                            Height="16px" onclick="lnkok_Click" style="margin-right: 0px; color: #FFFFFF;" 
                                            Width="43px">Ok</asp:LinkButton>
                                    </td>
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
                    <td colspan="7">
                       <%-- <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Bold="False" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" Width="726px" PageSize="15">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>--%>

                              <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" PageSize="15" Width="499px">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" Visible="False" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod3")) %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" 
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" Width="15px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Width="15px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf. Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="13" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Width="80px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Width="80px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Width="250px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" style="FONT-SIZE: 12px" Width="250px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="160px"></asp:Label>
                                                </td>
                                                <td class="style16">
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="14px" 
                                                        onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                                        style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                            </Columns>
                            <%--<RowStyle BackColor="#92AF5F" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#BBC684" />--%>
                            <RowStyle BackColor="#CAD8B1" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#66CCFF" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

