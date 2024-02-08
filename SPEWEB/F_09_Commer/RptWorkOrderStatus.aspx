<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderStatus.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptWorkOrderStatus" %>


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
        .style42
        {
            width: 237px;
        }
        .style43
        {
            width: 15px;
        }
        .style44
        {
            width: 12px;
        }
        .style45
        {
            width: 15px;
            height: 5px;
        }
        .style46
        {
            width: 39px;
            height: 5px;
        }
        .style47
        {
            width: 63px;
            height: 5px;
        }
        .style48
        {
            width: 12px;
            height: 5px;
        }
        .style49
        {
            width: 441px;
            height: 5px;
        }
        .style50
        {
            height: 5px;
        }
        .style57
        {
            height: 5px;
            width: 98px;
        }
        .style58
        {
            width: 98px;
        }
        .style62
        {
            width: 441px;
            height: 12px;
        }
        .style63
        {
            width: 15px;
        }
        .style67
        {
            width: 441px;
        }
        .style68
        {
            width: 300px;
            height: 12px;
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
                    ForeColor="Yellow" Text="WORK ORDER STATUS" Width="475px"
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
                                                        <table style="width:1018px;">
                                                            <tr>
                                                                <td class="style35" style="text-align: left">
                                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                        style="text-align: right; color: #FFFFFF;" Text="Order Name:" 
                                                                        Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style36" style="text-align: left">
                                                                    <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" 
                                                                        Font-Bold="True" Width="80px"></asp:TextBox>
                                                                </td>
                                                                <td class="style44" style="text-align: left">
                                                                    <asp:ImageButton ID="imgbtnFindOrder" runat="server" Height="17px" 
                                                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindOrder_Click" 
                                                                        Width="16px" />
                                                                </td>
                                                                <td class="style67" style="text-align: left" colspan="3">
                                                                    <asp:DropDownList ID="ddlOrderName" runat="server" Font-Bold="True" 
                                                                        Font-Size="12px" Width="300px">
                                                                    </asp:DropDownList>
                                                                    <cc1:ListSearchExtender ID="ddlOrderName_ListSearchExtender" runat="server" 
                                                                        QueryPattern="Contains" TargetControlID="ddlOrderName">
                                                                    </cc1:ListSearchExtender>
                                                                    <asp:Label ID="lblOrderdesc" runat="server" BackColor="White" 
                                                                        Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                                                        Width="300px"></asp:Label>
                                                                    <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#99FFCC" Height="20px" 
                                                                        onclick="lnkbtnOk_Click" style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: left" class="style58">
                                                                    </td>
                                                                <td style="text-align: left">
                                                                    </td>
                                                                <td style="text-align: left">
                                                                    </td>
                                                                <td style="text-align: left">
                                                                    </td>
                                                                <td style="text-align: left">
                                                                    </td>
                                                                <td style="text-align: left">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style46" style="text-align: left">
                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                        style="color: #FFFFFF; text-align: right;" Text="Date:" Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style47" style="text-align: left">
                                                                    <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" 
                                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td class="style48" style="text-align: left">
                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                        style="text-align: right; color: #FFFFFF;" Text="To:"></asp:Label>
                                                                </td>
                                                                <td class="style49" style="text-align: left" colspan="3">
                                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" 
                                                                        Font-Bold="False" Width="80px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td class="style57" style="text-align: left">
                                                                </td>
                                                                <td class="style50" style="text-align: left">
                                                                </td>
                                                                <td class="style50" style="text-align: left">
                                                                </td>
                                                                <td class="style50" style="text-align: left">
                                                                </td>
                                                                <td class="style50" style="text-align: left">
                                                                </td>
                                                                <td class="style50" style="text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style35" style="text-align: left">
                                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                        ForeColor="#993300" style="color: #FFFFFF; text-align: right;" Text="Page Size" 
                                                                        Visible="False" Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style36" style="text-align: left">
                                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" 
                                                                        BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                        Font-Size="14px" Height="14px" 
                                                                        onselectedindexchanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6" 
                                                                        RepeatDirection="Horizontal" style="text-align: left" Width="300px">
                                                                        <asp:ListItem>Requisition Basis</asp:ListItem>
                                                                        <asp:ListItem>Material Basis</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td class="style62" style="text-align: left">
                                                                    <asp:CheckBox ID="ChkBalance" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                        ForeColor="#660033" style="color: #FFFFFF;" Text="Without Zero Balance" 
                                                                        Width="156px" />
                                                                </td>
                                                                <td class="style62" style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left" class="style58">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                  
                                                </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                        </table>
                          <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="WorkIOrdStatus" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" 
                                                        AutoGenerateColumns="False" onpageindexchanging="gvReqStatus_PageIndexChanging" 
                                                        style="text-align: left" Width="889px">
                                                        <PagerSettings Position="Top" />
                                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px" 
                                                                        style="text-align: right" 
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvProjDesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvReqNo1" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' 
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRF No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvMrfNo" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDate" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>' 
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description of Materials">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvResDesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                                        Width="140px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvResUnit" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                                        Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px" 
                                                                        style="text-align: right" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="75px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Process">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvComqty" runat="server" Font-Size="11px" 
                                                                        style="text-align: right" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="75px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Completed">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvOrderQty" runat="server" Font-Size="11px" 
                                                                        style="text-align: right" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remaining Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px" 
                                                                        style="text-align: right" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                        Width="75px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px" 
                                                                        style="text-align: right" Width="75px"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Specification">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSpfDesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle BackColor="#666633" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                                    </asp:GridView>
                                    </td>
                                </tr>
                                          
                           </table>
                            </asp:View>
                            <asp:View ID="DetailsWorkIOrdStatus" runat="server">  
                              <table style="width:100%;">
                                <tr>
                                    <td>
                                    <asp:Panel ID="PanelDeWorkOrder" runat="server" ScrollBars="Horizontal" 
                                                Width="1000px" Visible="false">
                                         <asp:GridView ID="gvDeWorkOrdSt" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="831px" AllowPaging="True" 
                                        onpageindexchanging="gvDeWorkOrdSt_PageIndexChanging">
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
                                            
                                            <asp:TemplateField HeaderText="Order Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="200px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrdno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>' 
                                                        Width="70px" ></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Ref">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdRef" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Date ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdDat" runat="server" 
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReqNo" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno2")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRF No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMrfNo" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Appoved No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAppNo" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno2")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Suppliers Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSupDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Materials">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' 
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBrName" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                     <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>' 
                                                            Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdqty" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="35px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Approve Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppqty" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="35px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRate" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                 </FooterTemplate>
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Process">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEnUser" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Order Approved">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrAppUser" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprusername")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Print">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrPrUser" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orusername")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            
                                            
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerSettings Position="TopAndBottom" />
                                        <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                              </table>
                             </asp:View>
                           </asp:MultiView> 
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>



