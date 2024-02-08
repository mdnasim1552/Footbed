<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="IncomeReduced.aspx.cs" Inherits="SPEWEB.F_21_GAcc.IncomeReduced" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style19
        {
            width: 125px;
        }
        .style20
        {
            width: 57px;
        }
        .style21
        {
            width: 91px;
        }
        .style22
        {
            width: 14px;
        }
        .style23
        {
            width: 279px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:850px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="22px" 
                                ForeColor="White" Text="Variance Analysis" Font-Overline="True" 
                                Font-Underline="True"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                                </td>
                        <td class="style19">
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
                                    Font-Underline="True" ForeColor="White" onclick="lbtnPrint_Click" 
                                    Width="40px" BackColor="#003366" BorderColor="White" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Overline="True">PRINT</asp:LinkButton>
                           
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
                                    <td>
                                        &nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text=" MASTER LC :" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style20">
                                        <asp:TextBox ID="txtpmlcsrch" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style22">
                                        <asp:ImageButton ID="imgbtnFindPMlc" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindPMlc_Click" 
                                            Width="16px" />
                                    </td>
                                    <td class="style23">
                                        <asp:DropDownList ID="ddlMLc" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Width="320px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlMLc_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlMLc">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnShow_Click" 
                                            style="height: 17px">Show</asp:LinkButton>
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
                                    <td>
                                        &nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style20">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                            Width="80px">
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
                                    <td class="style22">
                                        &nbsp;</td>
                                    <td class="style23">
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
                        <asp:GridView ID="gvcostReduced" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" onpageindexchanging="gvcostReduced_PageIndexChanging" 
                            ShowFooter="True" style="text-align: left" Width="800px" 
                            onrowdatabound="gvcostReduced_RowDataBound">
                            <PagerSettings Position="Top" />
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
                                
                            
                                 
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" 
                                          Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                    %>'   Width="250px"></asp:Label>
                                    
                                    
                                    
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Qty">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBudgetedQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Qty">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actqty")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                                
                                 <asp:TemplateField HeaderText="Over Qty">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOverQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "overqty")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                     
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                      <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Budgeted Rate">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrdRat" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                      <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActRat" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actrate")).ToString("#,##0.000000;(#,##0.000000); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                 
                             
                                 <asp:TemplateField HeaderText="Qty Variance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQtyV" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qtyvar")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Rate Variance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRatVar" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratevar")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Total Amount (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTotal" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount (TK)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTotalTk" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamtTK")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="right" />
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

