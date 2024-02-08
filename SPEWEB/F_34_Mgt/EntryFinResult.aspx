<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryFinResult.aspx.cs" Inherits="SPEWEB.F_34_Mgt.EntryFinResult" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style52
        {
            width: 72px;
        }
        
        .style18
        {
            width: 53px;
        }
        
        .style53
        {
            width: 467px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="DAILY SALES & COLLECTION STATUS" Width="686px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>    
                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        
                
                
    
                                      <table style="width:100%;" __designer:mapid="c53">
                                    <tr __designer:mapid="c54">
                                        <td colspan="7" __designer:mapid="c55">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                                BorderWidth="1px">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td class="style52">
                                                            &nbsp;</td>
                                                        <td class="style18">
                                                            &nbsp;</td>
                                                        <td class="style23">
                                                            <asp:LinkButton ID="lbtnYearbgd" runat="server" BackColor="#000066" 
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lbtnYearbgd_Click" 
                                                                style="text-align: center; height: 17px;" Width="60px">Ok</asp:LinkButton>
                                                        </td>
                                                        <td class="style53">
                                                            <asp:Label ID="lblmsg02" runat="server" BackColor="Red" Font-Bold="True" 
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
                                    <tr __designer:mapid="c75">
                                        <td colspan="7" __designer:mapid="c76">
                                            <asp:GridView ID="gvFinResult" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="616px" onrowdatabound="gvFinResult_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>

                                                  <asp:TemplateField HeaderText="Sl. No.">
                                                      <FooterTemplate>
                                                          <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                              ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                                      </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNomon" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Description">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                           
                                                            <asp:HyperLink ID="HygvResDesc" runat="server" Font-Underline="false"  
                                                                ForeColor="Black" Target="_blank"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                                Width="280px"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   

                                                   
                                                    <asp:TemplateField HeaderText="amt1">
                                                        <FooterTemplate>
                                                           <%-- <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt1" runat="server" BackColor="Transparent" 
                                                                Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="90px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="amt2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt2" runat="server" BackColor="Transparent" 
                                                                Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="90px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <%--<asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt3">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt3" runat="server" BackColor="Transparent" 
                                                                 Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="90px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <%--<asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt4">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt4" runat="server" BackColor="Transparent"  
                                                                Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="90px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>

                                                         <FooterTemplate>
                                                            <%--<asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt5">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt5" runat="server" BackColor="Transparent"  
                                                                 Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="90px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <%--<asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerSettings Position="Top" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                   
                                </table>
                                     
</asp:Content>

