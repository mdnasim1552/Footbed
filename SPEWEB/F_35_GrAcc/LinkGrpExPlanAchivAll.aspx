<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpExPlanAchivAll.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpExPlanAchivAll" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style29
        {            text-align: center;
            width: 275px;
        }
        .style35
        {
            width: 39px;
        }
        .style36
        {
            width: 63px;
        }
        .style40
        {
            width: 631px;
            height: 9px;
        }
        .style41
        {
            width: 68px;
        }
        .style44
        {
            width: 12px;
        }
        .style62
        {
            width: 441px;
            height: 12px;
        }
        .style68
        {
            width: 300px;
            height: 12px;
        }
        .style16
        {
            color: #FFFFFF;
            text-align: right;
        }
        </style>
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 86%;">
        <tr>
            <td class="style40">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EXPORT PLAN VS ACHIVEMENT" Width="475px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style41">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        </table>
                
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width:100%;">
                                            
                                            <tr>
                                                <td class="style29">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table style="width:600px;">
                                                         <tr>
                                                                <td class="style35" style="text-align: left">
                                                                    <asp:Label ID="lblFDate" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                        style="text-align: right;" Text="From:" Width="100px" CssClass="style16" 
                                                                        Height="16px"></asp:Label>
                                                                </td>
                                                                <td class="style36" style="text-align: left">
                                                                    <asp:Label ID="lblDateRange" runat="server" BackColor="#000066" 
                                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                        Font-Size="12px" ForeColor="Yellow" Text="A. Sales" Width="300px"></asp:Label>
                                                                </td>
                                                                <td class="style44" style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td class="style68" style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td class="style62" style="text-align: left" colspan="2">
                                                                    &nbsp;</td>
                                                                <td class="style62" style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                           
                                                           
                                                            <tr>
                                                                <td class="style35" style="text-align: left">
                                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                        ForeColor="#993300" style="color: #FFFFFF; text-align: right;" 
                                                                        Text="Page Size" Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style36" style="text-align: left">
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
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style44" style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td class="style68" style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td class="style62" style="text-align: left" colspan="2">
                                                                    &nbsp;</td>
                                                                <td class="style62" style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                  
                                                </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                        </table>
                           <asp:Panel ID="PanelExPlan" runat="server">
                              <table style="width:600px;">
                                 
                                
                                 
                                <tr>
                                    <td colspan="11">
                                    <table style="width:600px;">
                                       
                                        <tr>
                                            <td></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            <asp:GridView ID="gvRptExPlnAch" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" 
                                                onpageindexchanging="gvRptExPlnAch_PageIndexChanging" ShowFooter="True" 
                                                    onrowdatabound="gvRptExPlnAch_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order no">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrdno" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdes")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Range">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrdDat" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "plandate")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Production Plan Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvProPlqty" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proplanqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipment Plan Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvShiPlqty" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipplnqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Production Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvProqty" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipment Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvShiQty" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Production %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvProPer" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercent")).ToString("#,##0.00;(#,##0.00); ")  %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipment %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvShiPer" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shippercent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Shipment Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvShiPType" runat="server" style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdesc")) %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerSettings Position="TopAndBottom" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                             
                                        
                                       
                                    </td>
                                </tr>
                              </table>
                            </asp:Panel>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>



