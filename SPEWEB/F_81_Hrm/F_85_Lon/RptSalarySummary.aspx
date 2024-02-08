<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptSalarySummary.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_85_Lon.RptSalarySummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style15
        {
            width: 44px;
        }
        .style16
        {
            width: 9px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
        }
        .style24
        {
            width: 10px;
        }
        .style33
        {
            width: 184px;
        }
        .style38
        {
            width: 78px;
        }
        .style41
        {
            width: 121px;
        }
        .style43
        {
            width: 116px;
        }
        .style44
        {
            width: 96px;
        }
    </style>
    <link href="../../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Salary Summary" Width="667px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;" Height="16px"></asp:Label>
            </td>
            <td class="style33">
                <asp:Label ID="lblprint" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" OnClick="lbtnPrint_Click"
                    CssClass="button" ForeColor="White">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="10">
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style16">
                                        &nbsp;
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="Company:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnCompany_Click" Width="16px" />
                                    </td>
                                    <td colspan="7" align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="12px"  Width="300px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="16px" OnClick="lnkbtnShow_Click" Style="text-align: center;" Width="50px">Ok</asp:LinkButton>
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
                                
                               
                                <tr>
                                    <td class="style16">
                                        &nbsp;
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style44">
                                        <asp:TextBox ID="txtfromdate" runat="server" Width="100px" CssClass="txtboxformat"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txttodate" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style38" align="left">
                                        &nbsp;</td>
                                    <td class="style41">
                                        &nbsp;</td>
                                    <td class="style41">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White"></asp:Label>
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
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <asp:MultiView ID="MultiView1" runat="server">
                            
                            <asp:View ID="TopSheet" runat="server">
                                <asp:GridView ID="gvSalSum" runat="server" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvSalSum_PageIndexChanging" ShowFooter="True" 
                                    Width="500px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name" FooterText="Total:">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProName0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="White"/>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           
                                            <ItemStyle Font-Size="12px"  />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Cur.Emp.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpCur" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curempno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lgvFTCurEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                          <HeaderTemplate> 
                                                 <asp:Label ID="lgvHTCurMEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cur.Amuont">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtCur" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderTemplate> 
                                                 <asp:Label ID="lgvHTCurMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvFTCurMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                           <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre.Emp.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpPre" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preempno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                              <FooterTemplate>
                                                <asp:Label ID="lgvFTPreEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderTemplate> 
                                                 <asp:Label ID="lgvHTPreMEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre.Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtPre" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prepay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate> 
                                                 <asp:Label ID="lgvHTPreMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTPreMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                        Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="12px" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
               
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
