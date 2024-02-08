<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptRequsitionStatus.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptRequsitionStatus" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style36
    {
            width: 17px;
        }
        .style39
        {
            width: 57px;
        }
        .style40
        {
            width: 45px;
        }
        .style41
        {
            width: 575px;
            height: 9px;
        }
        .style42
        {
            color: #FFFFFF;
        }
        .style43
    {
        width: 156px;
    }
        .style45
        {
            width: 86px;
        }
        .style46
        {
            width: 63px;
        }
        .style47
        {
            width: 15px;
        }
        .style54
        {
            width: 134px;
        }
        .style55
        {
            width: 429px;
        }
        .style56
        {
            width: 15px;
            height: 23px;
        }
        .style57
        {
            width: 86px;
            height: 23px;
        }
        .style58
        {
            width: 63px;
            height: 23px;
        }
        .style59
        {
            width: 17px;
            height: 23px;
        }
        .style60
        {
            width: 429px;
            height: 23px;
        }
        .style61
        {
            width: 57px;
            height: 23px;
        }
        .style62
        {
            width: 45px;
            height: 23px;
        }
        .style63
        {
            height: 23px;
        }
        .style65
        {
            width: 217px;
        }
        .style67
        {
            width: 93px;
        }
        .style68
        {
            width: 2px;
        }
        .style69
        {
            width: 26px;
        }
        .style70
        {
            width: 42px;
        }
        </style>
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 92%;">
        <tr>
            <td class="style41">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="REQUISITION STATUS REPORT" Width="419px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style43">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                                </td>
            <td class="style54">
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
                    onclick="lbtnPrint_Click" CssClass="button" Font-Names="Verdana" 
                    style="color: #FFFFFF">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                
                
                                       
                <table style="width: 100%;">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width:100%; margin-bottom: 0px;">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style47">
                                                    &nbsp;</td>
                                                <td class="style45">
                                                    &nbsp;</td>
                                                <td class="style46">
                                                    &nbsp;</td>
                                                <td class="style36">
                                                    &nbsp;</td>
                                                <td class="style67" colspan="6">
                                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" 
                                                        BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="14px" Height="14px" RepeatColumns="6" RepeatDirection="Horizontal" 
                                                        Width="300px">
                                                        <asp:ListItem>Requisition  Basis </asp:ListItem>
                                                        <asp:ListItem>Materials Basis</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td class="style55">
                                                    <asp:CheckBox ID="ChkBalance" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="#660033" style="color: #FFFFFF" Text="Without Zero Balance" 
                                                        Width="150px" />
                                                </td>
                                                <td class="style55">
                                                    &nbsp;</td>
                                                <td class="style39">
                                                    &nbsp;</td>
                                                <td class="style40">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style47">
                                                    &nbsp;</td>
                                                <td class="style45">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="text-align: right; color: #FFFFFF;" Text="Order Name:" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style46">
                                                    <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" 
                                                        Font-Bold="True" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style36">
                                                    <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindProject_Click" 
                                                        Width="16px" />
                                                </td>
                                                <td class="style55" colspan="8">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" Width="300px">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" 
                                                        QueryPattern="Contains" TargetControlID="ddlProjectName">
                                                    </cc1:ListSearchExtender>
                                                    <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#99FFCC" Height="20px" 
                                                        onclick="lnkbtnOk_Click" style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                                </td>
                                                <td class="style39">
                                                    &nbsp;</td>
                                                <td class="style40">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style56">
                                                </td>
                                                <td class="style57">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style42" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: right" Text="Date:" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style58">
                                                    <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style59">
                                                    <asp:Label ID="Label6" runat="server" CssClass="style42" Font-Bold="True" 
                                                        style="text-align: right" Text="To:" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="style60" colspan="8">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style61">
                                                </td>
                                                <td class="style62">
                                                </td>
                                                <td class="style63">
                                                </td>
                                                <td class="style63">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style47">
                                                    &nbsp;</td>
                                                <td class="style45">
                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td class="style46">
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
                                                <td class="style36">
                                                    <asp:Label ID="Label8" runat="server" CssClass="style42" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: right" Text="Req:"></asp:Label>
                                                </td>
                                                <td class="style67">
                                                    <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="txtboxformat" 
                                                        Font-Bold="True" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style69">
                                                    <asp:ImageButton ID="imgbtnFindRequiSition" runat="server" Height="16px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindRequiSition_Click" 
                                                        Width="16px" />
                                                </td>
                                                <td class="style70">
                                                    &nbsp;</td>
                                                <td class="style68">
                                                    &nbsp;</td>
                                                <td class="style67">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style65">
                                                    &nbsp;</td>
                                                <td class="style55">
                                                    &nbsp;</td>
                                                <td class="style39">
                                                    &nbsp;</td>
                                                <td class="style40">
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
                                <td>
                                    <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" Width="901px" 
                                        onpageindexchanging="gvReqStatus_PageIndexChanging" >
                                        <PagerSettings Position="Top" />
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProjDesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrfNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Materials">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvComqty" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
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

                                           
                                             <asp:TemplateField HeaderText="Entry User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEntryUser" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Aproved Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvaprovdat" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>' 
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="App. User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAprvuser" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ausrname")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" BackColor="#666633" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
</asp:Content>




