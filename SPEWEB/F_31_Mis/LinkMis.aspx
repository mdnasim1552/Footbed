<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkMis.aspx.cs" Inherits="SPEWEB.F_31_Mis.LinkMis" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


    .style50
    {
        color: white;
    }
        .style51
        {
            width: 40px;
        }
        .style52
        {
            width: 69px;
        }
        .style53
        {
            width: 71px;
        }
        .style54
        {
            width: 174px;
        }
        .style55
        {
            width: 593px;
        }
        .style58
        {
            width: 81px;
        }
                        
        .style60
        {
            height: 21px;
        }
        .style61
        {
            width: 668px;
        }
        .style63
        {
            width: 716px;
        }
        .style64
        {
            width: 1945px;
        }
        .style65
        {
            width: 1236px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeadtitle" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT WISE COLLECTION BREAK DOWN REPORT" 
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style51">
                                                <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" 
                                                    Font-Size="12px" style="text-align: left" Text="Bill No:" Width="70px"></asp:Label>
                                            </td>
                                            <td class="style52">
                                                <asp:Label ID="lblbillno" runat="server" BackColor="#000066" 
                                                    BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                    Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style53">
                                                <asp:Label ID="Label5" runat="server" CssClass="style50" Font-Bold="True" 
                                                    Font-Size="12px" style="text-align: left" Text="Project Name:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style54">
                                                <asp:Label ID="lblProjectName" runat="server" BackColor="#000066" 
                                                    BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                    Font-Size="12px" ForeColor="Yellow" Width="200px"></asp:Label>
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
                            <td colspan="12">
                                <asp:GridView ID="gvPurBilltk" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" Width="734px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNo0" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat1" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefno0" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvduration" runat="server" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0; (#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cumulative">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvduration0" runat="server" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cduration")).ToString("#,##0; (#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials5" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' 
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit4" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' 
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecification0" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty3" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate2" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamount0" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier2" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusername" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                </asp:View>
                        <asp:View ID="ViewResCostDetails" runat="server">
                             <table style="width: 100%;">
                                 <tr>
                                     <td colspan="12">
                                         <asp:Panel ID="Panel2" runat="server">
                                             <table style="width: 100%;">
                                                 <tr>
                                                     <td class="style51">
                                                         &nbsp;</td>
                                                     <td class="style52">
                                                         &nbsp;</td>
                                                     <td class="style53">
                                                         <asp:Label ID="Label7" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="Description:" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style55" colspan="13">
                                                         <asp:Label ID="lblResName" runat="server" BackColor="#000066" 
                                                             BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                             Font-Size="12px" ForeColor="Yellow" Width="400px"></asp:Label>
                                                     </td>
                                                     <td class="style63">
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
                                                     <td class="style51">
                                                         &nbsp;</td>
                                                     <td class="style52">
                                                         &nbsp;</td>
                                                     <td class="style53">
                                                         <asp:Label ID="Label10" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:Label ID="lblfrmdate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style55">
                                                         <asp:Label ID="Label8" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="To:"></asp:Label>
                                                     </td>
                                                     <td class="style64">
                                                         <asp:Label ID="lbltodate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" Height="16px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style65">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style61">
                                                         &nbsp;</td>
                                                     <td class="style63">
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
                                     <asp:Panel ID="Panel10" runat="server" Width="1260px">
                                         <asp:GridView ID="gvComCost" runat="server" AutoGenerateColumns="False" 
                                             ShowFooter="True" Width="616px">
                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                             <Columns>
                                                 <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                                     <HeaderTemplate>
                                                         <table style="width:47%;">
                                                             <tr>
                                                                 <td class="style58">
                                                                     <asp:Label ID="Label9" runat="server" Font-Bold="True" Height="16px" 
                                                                         Text="Description" Width="180px"></asp:Label>
                                                                 </td>
                                                                 <td class="style60">
                                                                     &nbsp;</td>
                                                                 <td>
                                                                     <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" 
                                                                         BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                         ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </HeaderTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvActDescCost" runat="server" 
                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                             Width="200px"></asp:Label>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                         HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Total">
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvtopamtcost" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topamt")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="60px"></asp:Label>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P1">
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc1" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P2">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc2" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P3">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc3" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P4">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc4" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P5">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc5" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P6">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc6" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P7">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc7" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P8">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc8" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P9">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc9" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC9" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P10">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc10" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC10" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P11">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc11" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC11" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P12">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc12" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC12" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P13">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc13" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC13" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P14">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc14" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC14" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P15">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc15" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC15" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P16">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc16" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC16" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P17">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc17" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC17" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P18">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc18" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC18" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P19">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc19" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC19" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P20">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc20" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC20" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P21">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc21" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC21" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P22">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvp22" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC22" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P23">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc23" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC23" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P24">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc24" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC24" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P25">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc25" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC25" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P26">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc26" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC26" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P27">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc27" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC27" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P28">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc28" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC28" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P29">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc29" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC29" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P30">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc30" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC30" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P31">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc31" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC31" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P32">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc32" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC32" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P33">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc33" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC33" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P34">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc34" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC34" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P35">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc35" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC35" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P36">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc36" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC36" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P37">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc37" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC37" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P38">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc38" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC38" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P39">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc39" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC39" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P40">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc40" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC40" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P41">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc41" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p41")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC41" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P42">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc42" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p42")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC42" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P43">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc43" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p43")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC43" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P44">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc44" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p44")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC44" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P45">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc45" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p45")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC45" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P46">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc46" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p46")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC46" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P47">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc47" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p47")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC47" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P48">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc48" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p48")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC48" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P49">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc49" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p49")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC49" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P50">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc50" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p50")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC50" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P51">
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC51" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc51" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p51")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P52">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc52" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p52")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC52" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P53">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc53" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p53")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC53" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P54">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc54" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p54")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC54" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P55">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc55" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p55")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC55" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P56">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc56" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p56")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC56" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P57">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc57" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p57")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC57" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P58">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc58" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p58")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC58" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P59">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc59" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p59")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC59" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P60">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc60" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p60")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC60" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P61">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc61" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p61")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC61" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P62">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc62" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p62")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC62" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P63">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc63" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p63")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC63" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P64">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc101" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p64")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC64" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P65">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc65" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p65")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC65" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P66">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc66" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p66")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC66" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P67">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc67" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p67")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC67" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P68">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc68" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p68")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC68" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P69">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc69" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p69")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC69" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P70">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc70" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p70")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC70" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P71">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc71" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p71")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC71" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P72">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvp23" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p72")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC72" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P73">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc73" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p73")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC73" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P74">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc74" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p74")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC74" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P75">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc75" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p75")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC75" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P76">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc76" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p76")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC76" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P77">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc77" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p77")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC77" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P78">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc78" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p78")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC78" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P79">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc79" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p79")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC79" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P80">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc80" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p80")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC80" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P81">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc81" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p81")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC81" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P82">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc82" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p82")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC82" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P83">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc83" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p83")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC83" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P84">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc84" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p84")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC84" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P85">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc85" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p85")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC85" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P86">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc86" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p86")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC86" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P87">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc87" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p87")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC87" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P88">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc88" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p88")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC88" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P89">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc89" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p89")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC89" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P90">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc90" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p90")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC90" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P91">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc91" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p91")).ToString("#,##0;(#,##0); ")%>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC91" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P92">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc92" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p92")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC92" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P93">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc93" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p93")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC93" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P94">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc94" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p94")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC94" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P95">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc95" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p95")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC95" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P96">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc96" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p96")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC96" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P97">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc97" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p97")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC97" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P98">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc98" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p98")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC98" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P99">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc99" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p99")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC99" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="P100">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvpc100" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p100")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="55px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFPC100" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="White" style="text-align: right" Width="55px"></asp:Label>
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
                                         </asp:Panel>
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
                             </asp:View>

            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

