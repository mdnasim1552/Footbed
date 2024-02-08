<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpAccDailyTransaction.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.RptGrpAccDailyTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/Login.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 91px;
        }
        .style6
        {
            width: 53px;
        }
        .style7
        {
            width: 70px;
        }
        .style8
        {
            width: 39px;
        }
        .style9
        {
            width: 82px;
        }
        .style42
        {
            width: 661px;
        }
        .style54
        {
            width: 78px;
        }
        .style53
        {
            width: 32px;
        }
        .style56
        {
            width: 87px;
        }
        .style57
        {
            width: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="DAILY TRANSACTION INFORMATION VIEW/EDIT" Width="500px" Style="border-bottom: 1px solid WHITE;
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
                    Style="color: #FFFFFF" BackColor="#000066" BorderColor="White" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" ForeColor="White">PRINT</asp:LinkButton>
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
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style6">
                                <asp:Label ID="lblDatefrom" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="White" style="text-align: right" Text=" From:" Width="80px"></asp:Label>
                            </td>
                            <td class="style7">
                                <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblDateto" runat="server" BorderStyle="None" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White" style="text-align: right" Text="To:" 
                                    Width="50px"></asp:Label>
                            </td>
                            <td class="style9">
                                <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style57">
                                <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Size="12px" Height="16px" onclick="lbtnShow_Click" 
                                    style="text-align: center; color: #FFFFFF;">Show</asp:LinkButton>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="18px" 
                                            ForeColor="Yellow" style="text-align:center" Text="Please wait . . . . . . ." 
                                            Width="218px"></asp:Label>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
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
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="12">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ViewDailyTransaction" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style42" colspan="7">
                                    <asp:Label ID="lblTransactionTitle" runat="server" Font-Bold="True" Font-Size="16px" 
                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99" 
                                        Text="Transaction Listing" Width="569px" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="width:100px;">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style54">
                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" Style="text-align: right;" Text="Page Size :" Visible="False" 
                                        Width="70px"></asp:Label>
                                </td>
                                <td class="style53">
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>600</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblVoucher" runat="server" Font-Size="12px" ForeColor="White" 
                                        style="font-weight: 700; text-align: right" Text="Vouchar Type :" 
                                        Visible="False" Width="130px"></asp:Label>
                                </td>
                                <td class="style56">
                                    <asp:DropDownList ID="ddlVouchar" runat="server" BackColor="#CCFFCC" 
                                        Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" Visible="False">
                                        <asp:ListItem>BC</asp:ListItem>
                                        <asp:ListItem>BD</asp:ListItem>
                                        <asp:ListItem>CC</asp:ListItem>
                                        <asp:ListItem>CD</asp:ListItem>
                                        <asp:ListItem>CT</asp:ListItem>
                                        <asp:ListItem>JV</asp:ListItem>
                                        <asp:ListItem Selected="True">ALL Voucher</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style42" width="200px">
                                    <asp:ImageButton ID="imgbtnSearchVoucher" runat="server" Height="16px" 
                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnSearchVoucher_Click" 
                                        Width="16px" Visible="False" />
                                </td>
                                <td class="style42" width="300px">
                                    &nbsp;</td>
                                <td class="style42">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="width:100px;">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvtranlsit" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" onpageindexchanging="gvtranlsit_PageIndexChanging" 
                                      OnRowDataBound="gvtranlsit_RowDataBound" 
                                        PageSize="15" ShowFooter="True" Width="931px">
                                        <PagerSettings Position="TopAndBottom" />
                                        <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" 
                                                        Style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCompany" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsnam")) %>' 
                                                        Width="120px" Font-Bold="true" Font-Size="16px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' 
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher #" FooterText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvnum1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAcRsCode" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAcRsDesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>' 
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res. Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvInAmt" runat="server" Style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDram" runat="server" Style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDram" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvCram" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtgvFCram" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque/Ref #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRefnum" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpostusername" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbydesc")) %>' 
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                      <asp:View ID="ViewBgsVsAchievement" runat="server">
                       <table style="width: 100%;">
                            <tr>
                                <td align="right" class="style60">
                                    <asp:Label ID="lblVoucher0" runat="server" Font-Size="12px" ForeColor="White" Style="font-weight: 700;
                                        text-align: right" Text="Company Name :" Width="115px" Height="16px"></asp:Label>
                                </td>
                                <td class="style59" align="left">
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="newStyle1" 
                                        Font-Bold="True" Font-Size="11px" Height="21px" Width="400px">
                                    </asp:DropDownList>  <asp:Label ID="lblCompanyDesc" runat="server" BackColor="White" 
                                        CssClass="newStyle1" Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                                        Height="18px" style="font-weight: 700" Visible="False"></asp:Label>
                                </td>
                                <td class="style4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            
                        </table>
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

