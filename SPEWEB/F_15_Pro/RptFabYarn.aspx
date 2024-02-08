<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptFabYarn.aspx.cs" Inherits="SPEWEB.F_15_Pro.RptFabYarn" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style19
    {
        width: 46px;
    }
    .style20
    {
        width: 8px;
    }
    .style21
    {
        width: 17px;
    }
    .style22
    {
        width: 7px;
    }
        .style23
        {
            width: 109px;
        }
        .style24
        {
            width: 148px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="FABRIC REQUISITION INFORMATION" Width="590px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td class="style23">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style24">
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
                    style="color: #FFFFFF; height: 17px; margin-left: 2px;" BackColor="#003366" 
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
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td class="style19">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text=" MASTER LC :" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtpmlcsrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style22">
                                        <asp:ImageButton ID="imgbtnFindPMlc" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindPMlc_Click" 
                                            Width="16px" />
                                    </td>
                                    <td colspan="40">
                                        <asp:DropDownList ID="ddlMLc" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" 
                                            onselectedindexchanged="ddlMLc_SelectedIndexChanged" Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="lsePMLc" runat="server" QueryPattern="Contains" 
                                            TargetControlID="ddlMLc">
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
                                    <td class="style20">
                                    </td>
                                    <td class="style19">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="ORDER NO:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtordercsrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style22">
                                        <asp:ImageButton ID="imgbtnFindOrder" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindOrder_Click" 
                                            Width="16px" />
                                    </td>
                                    <td colspan="40">
                                        <asp:DropDownList ID="ddlOrder" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlOrder_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlOrder">
                                        </cc1:ListSearchExtender>
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnShow_Click" 
                                            style="height: 17px">Show</asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td class="style19">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="From:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtfrmDate" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style22">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttoDate" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttoDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
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
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="Fabric" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvfab" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" onpageindexchanging="gvfab_PageIndexChanging" 
                                                ShowFooter="True" style="text-align: left" Width="776px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvOrder" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdesc")) %>' 
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfabno" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fabno1")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvStyleDesc" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>' 
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColorDesc" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSize" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>' 
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fabric Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfabdate" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" 
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fabdate")).ToString("dd-MMM-yyyy") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fabric Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfabtype" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fabtype")) %>' 
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GSM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgsm" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gsm")) %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvUnit1" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFfQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fin Dia">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfindia" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "findia")) %>' 
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
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
                                </table>
                            </asp:View>
                             <asp:View ID="Yarn" runat="server">
                                 <table style="width:100%;">
                                     <tr>
                                         <td>
                                             <asp:GridView ID="gvYarn" runat="server" AllowPaging="True" 
                                                 AutoGenerateColumns="False" onpageindexchanging="gvYarn_PageIndexChanging" 
                                                 ShowFooter="True" style="text-align: left" Width="776px">
                                                 <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblserialnoid0" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                         <ItemStyle Font-Size="12px" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Order">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvOrder0" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdesc")) %>' 
                                                                 Width="120px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Requsition. No">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvreq" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fabno1")) %>' 
                                                                 Width="80px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Req. Date">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvyrndate" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" 
                                                                 Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fabdate")).ToString("dd-MMM-yyyy") %>' 
                                                                 Width="80px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="left" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Yarn Com">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvyrncom" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yrncom")) %>' 
                                                                 Width="120px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="left" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Yarn Count">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvyrncnt" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yrncnt")) %>' 
                                                                 Width="80px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="left" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Fabric Qty">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvfabQty" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFfabQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Yarn Qty">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvyrnQty" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "yrnqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                           <FooterTemplate>
                                                               <asp:Label ID="lblgvFyQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                   ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                           </FooterTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Inc. Qty">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgviyrnQty" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iyrnqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                          <FooterTemplate>
                                                               <asp:Label ID="lblgvFiyQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                   ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                           </FooterTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Dec. Qty">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvdyrnQty" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dyrnqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                          
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                               <asp:Label ID="lblgvFdyQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                   ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                           </FooterTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Total Qty">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvtyrnQty" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tyrnqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                          <FooterTemplate>
                                                               <asp:Label ID="lblgvFtyQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                   ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                           </FooterTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Excess(%)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvexcess" runat="server" BackColor="Transparent" 
                                                                 BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excess")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="60px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="right" />
                                                     </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Remarks">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvremrkd" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yrnremrk")) %>' 
                                                                 Width="80px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
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
                                 </table>
                            </asp:View>
                        </asp:MultiView>
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


