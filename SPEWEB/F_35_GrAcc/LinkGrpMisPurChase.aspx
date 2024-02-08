<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisPurChase.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpMisPurChase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style8
        {
            width: 64px;
        }
        .style93
        {
            width: 20px;
        }
        .style94
        {
            width: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Management Interface" Width="600px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style22">
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
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        </table>
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style8">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style93">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" TabIndex="2" Width="85px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style94">
                                        <asp:Label ID="lblDateRange" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: right;" Text="Date:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Text="A. Sales" Width="300px"></asp:Label>
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
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewreqAppStatus" runat="server">
                                <asp:GridView ID="gvReqStatusAp" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="gvReqStatusAp_PageIndexChanging" ShowFooter="True" 
                                    Width="501px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <FooterTemplate>
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDescap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNoap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrfNoap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDateap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDescap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnitap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQtyap" runat="server" Font-Size="11px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprrateap" runat="server" Font-Size="11px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvapramt" runat="server" Font-Size="11px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvreqapAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDescap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEntryUserap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aproved Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovdatap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>' 
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App. User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprvuserap" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ausrname")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewWorkOrder" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:Panel ID="PanelDeWorkOrder" runat="server" ScrollBars="Horizontal" 
                                              Width="1000px">
                                                <asp:GridView ID="gvDeWorkOrdSt" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" 
                                                    onpageindexchanging="gvDeWorkOrdSt_PageIndexChanging" ShowFooter="True" 
                                                    Width="831px">
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
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPactdesc" runat="server" Font-Bold="true" 
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvOrdno" runat="server" 
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>' 
                                                                    Width="70px"></asp:Label>
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
                                                                <asp:Label ID="lgvditem" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                    ForeColor="White" HorizontalAlign="Left" style="text-align: right" Text="Total" 
                                                                    Width="70px"></asp:Label>
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
                                                                <asp:Label ID="lgvOrdqty" runat="server" style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="35px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvAppqty" runat="server" style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="35px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvRate" runat="server" style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvTAmt" runat="server" style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="70px"></asp:Label>
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
                                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
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
                                <asp:View ID="ViewPurchase" runat="server">
                                    <asp:GridView ID="gvPurStatus" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" onpageindexchanging="gvPurStatus_PageIndexChanging" 
                                        ShowFooter="True" Width="831px">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrrNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Ref.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrrNo0" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>' 
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Date ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrrDate" runat="server" 
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>' 
                                                        Width="72px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrfNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Ref.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdref0" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrref")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBillNo" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                        Width="135px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvqty" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate0" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate1" runat="server" style="text-align: left" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>' 
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
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
                                </asp:View>

                                <asp:View ID="ViewBillCOnfirmation" runat="server">
                                      <asp:GridView ID="gvBillStatus" runat="server" AllowPaging="True" 
                                          AutoGenerateColumns="False" 
                                          onpageindexchanging="gvBillStatus_PageIndexChanging" ShowFooter="True" 
                                          Width="831px">
                                          <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                          <Columns>
                                              <asp:TemplateField HeaderText="Sl">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" 
                                                          style="text-align: right" 
                                                          Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Bill No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvBillNobill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Bill Date">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvBilldate" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>

                                               <asp:TemplateField HeaderText="MRR No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvMrrNobill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Order No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvOrdNobill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                             
                                             

                                              <asp:TemplateField HeaderText="Req No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvReqNobill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>

                                               <asp:TemplateField HeaderText="MRF No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvMrfNobill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                          Width="75px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              

                                              <asp:TemplateField HeaderText="Project Desc.">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblgvprojectdescbill" runat="server" Height="16px" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                          Width="140px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                             
                                             
                                             
                                             
                                              <asp:TemplateField HeaderText="Material Name">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvMaterialsbill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                          Width="135px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Unit">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvUnitbill" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                          Width="50px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Specification">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvSpecification" runat="server" 
                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Qty">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvbillqty" runat="server" style="text-align: right" 
                                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                          Width="50px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Rate">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvbillrate" runat="server" style="text-align: right" 
                                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                          Width="55px"></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Amount">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvbillAmt" runat="server" style="text-align: right" 
                                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                          Width="70px"></asp:Label>
                                                  </ItemTemplate>
                                                  <FooterTemplate>
                                                      <asp:Label ID="lgvFbillAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                          ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                  </FooterTemplate>
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

