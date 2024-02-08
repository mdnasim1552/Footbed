<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccBudget.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccBudget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style40
        {
            color: #FFFFFF;
            text-align: center;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 11px;
            font-weight: normal;
            margin-right: 0px;
        }
        .style41
        {
            width: 48px;
        }
        .style43
        {
            width: 75px;
        }
        .style44
        {
            width: 62px;
        }
        .style45
        {
            width: 13px;
        }
        .style46
        {
            width: 74px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="WORKING BUDGET Vs. ACHIEVEMENT VIEW/EDIT" Width="500px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style43">
                                        &nbsp;</td>
                                    <td class="style41">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Style="text-align: right" Text="From.:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style45">
                                        <asp:Label ID="Label6" runat="server" CssClass="style40" Font-Bold="True" 
                                            Font-Size="12px" Style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style46">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style40" 
                                            Font-Bold="True" Font-Size="12px" OnClick="lbtnShow_Click" Width="56px" 
                                            Text="Show" Height="16px"></asp:LinkButton>
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
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvbgdvse" runat="server" AllowPaging="false" 
                                    AutoGenerateColumns="False" 
                                    OnRowDataBound="gvbgdvse_RowDataBound" ShowFooter="True" Width="501px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" 
                                                    Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                       
                                       
                                        <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:Label ID="lblgvAcDesc" runat="server" 
                                                  Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'   Width="300px">
                                                   </asp:Label></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget"><ItemTemplate><asp:Label ID="lgvbgdamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="90px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="right" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual "><ItemTemplate><asp:Label ID="lgvacamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="90px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Difference"><ItemTemplate><asp:Label ID="txtgvdiffamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /><ItemStyle HorizontalAlign="right" /></asp:TemplateField>
                                       
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                             <asp:View ID="DetailsBgd" runat="server">
                                 <asp:GridView ID="gvbgdvsAcd" runat="server" AllowPaging="false" 
                                     AutoGenerateColumns="False" OnRowDataBound="gvbgdvsAcd_RowDataBound" 
                                     ShowFooter="True" Width="501px">
                                     <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                     <Columns>
                                         <asp:TemplateField HeaderText="Sl.No.">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" 
                                                     Style="text-align: right" 
                                                     Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description">
                                             <ItemTemplate>
                                                 
                                                 
                                                 <asp:Label ID="lblgvAcDescDet" runat="server" 
                                                 Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                    %>'   Width="300px">
                                                   </asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Budget">
                                             <ItemTemplate>
                                                 <asp:Label ID="lgvbgdamtDet" runat="server" Style="text-align: right" 
                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>' 
                                                     Width="90px"></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="right" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Actual ">
                                             <ItemTemplate>
                                                 <asp:Label ID="lgvacamtDet" runat="server" Style="text-align: right" 
                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>' 
                                                     Width="90px"></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                             <ItemStyle HorizontalAlign="Right" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Difference">
                                             <ItemTemplate>
                                                 <asp:Label ID="txtgvdiffamtDet" runat="server" BackColor="Transparent" 
                                                     BorderStyle="None" Style="text-align: right" 
                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffamt")).ToString("#,##0;(#,##0); ") %>' 
                                                     Width="80px"></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                             <ItemStyle HorizontalAlign="right" />
                                         </asp:TemplateField>
                                     </Columns>
                                     <FooterStyle BackColor="#333333" />
                                     <PagerStyle HorizontalAlign="Center" />
                                     <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                         ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                     <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                     <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                 </asp:GridView>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

