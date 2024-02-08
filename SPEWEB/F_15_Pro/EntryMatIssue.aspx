<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryMatIssue.aspx.cs" Inherits="SPEWEB.F_15_Pro.EntryMatIssue" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style21
        {
            width: 24px;
        }
        .style20
        {
            width: 80px;
        }
        .style25
        {
            width: 14px;
        }
        .style24
        {
            width: 450px;
        }
        .style40
        {
            width: 24px;
            height: 19px;
        }
        .style41
        {
            width: 80px;
            height: 19px;
        }
        .style42
        {
            width: 66px;
            height: 19px;
        }
        .style43
        {
            width: 14px;
            height: 19px;
        }
        .style44
        {
            width: 450px;
            height: 19px;
        }
        .style45
        {
            height: 19px;
        }
        .style48
        {
            width: 93px;
        }
        .style47
        {
            width: 65px;
        }
        .style49
        {
            width: 10px;
        }
        .style50
        {
            width: 68px;
        }
        .style52
        {
            width: 42px;
        }
        .style53
        {
            width: 9px;
        }
        .style54
        {
            width: 66px;
        }
        .style55
        {
            width: 31px;
        }
        .style56
        {
            width: 125px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="MATERIALS ISSUE INFORMATION" Width="500px"
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
                                    <td class="style21">
                                        &nbsp;</td>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td class="style22">
                                        <asp:LinkButton ID="lbtnPreIsue" runat="server" BorderStyle="None" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px" 
                                            onclick="lbtnPreIsue_Click">Previous Issue</asp:LinkButton>
                                    </td>
                                    <td class="style25">
                                        &nbsp;</td>
                                    <td class="style24" colspan="7">
                                        <asp:DropDownList ID="ddlPreIssueNo" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlPreIssueNo_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlPreIssueNo">
                                        </cc1:ListSearchExtender>
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
                                </tr>
                                <tr>
                                    <td class="style40">
                                    </td>
                                    <td class="style41">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Order No. :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style42">
                                        <asp:TextBox ID="txtOrdsrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style43">
                                        <asp:ImageButton ID="imgbtnFindOrd" runat="server" Height="20px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindOrd_Click" 
                                            Width="20px" />
                                    </td>
                                    <td class="style44" colspan="7">
                                        <asp:DropDownList ID="ddlOrder" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="lsePMLc0" runat="server" QueryPattern="Contains" 
                                            TargetControlID="ddlOrder">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblOrder" runat="server" __designer:wfdid="w3" 
                                            style="BORDER-RIGHT: aqua 1px solid; BORDER-TOP: aqua 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14pt; BORDER-LEFT: aqua 1px solid; COLOR: yellow; BORDER-BOTTOM: aqua 1px solid; BACKGROUND-COLOR: #330000; TEXT-ALIGN: left" 
                                            Visible="False" Width="320px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtnOk_Click" style="width: 25px; height: 17px; text-align: center;" 
                                            Width="40px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                    <td class="style45">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        &nbsp;</td>
                                    <td class="style20">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Issue No:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style22">
                                        <asp:TextBox ID="txtIssueno" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style47">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style24">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="14px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        &nbsp;</td>
                                    <td class="style24">
                                        &nbsp;</td>
                                    <td class="style49">
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
                        <asp:Panel ID="PnlReslist" runat="server" BorderColor="Yellow" 
                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style52">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Resource List:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style54">
                                        <asp:TextBox ID="txtRessrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style55">
                                        <asp:ImageButton ID="imgbtnFindRes" runat="server" Height="20px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="20px" 
                                            onclick="imgbtnFindRes_Click" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlResList_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlResList">
                                        </cc1:ListSearchExtender>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="12px"  
                                            ForeColor="White" style="height: 17px; text-align: center" 
                                            onclick="lbtnSelect_Click">Select</asp:LinkButton>
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
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvMat" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" onpageindexchanging="gvMat_PageIndexChanging" 
                            ShowFooter="True" style="text-align: left" Width="580px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                            <Columns>
                            <asp:TemplateField HeaderText="Serial No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtnTotal_Click" style="text-align: right">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Font-Size="12px"  Font-Bold="true"/>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Materials Description ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" class="UpdateButton" onclick="lbtnUpdate_Click" 
                                            style="text-align: right">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>' 
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderText="Unit">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="60px" BackColor="Transparent" BorderStyle="None" 
                                            style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                 
                      
                                <asp:TemplateField HeaderText="Qty">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="Solid" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Center" />
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
